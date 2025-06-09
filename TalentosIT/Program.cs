using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentosIT.Data;
using TalentosIT.Repository;
using TalentosIT.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext com retry
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null
        );
    })
);

// MVC + sessões + HttpContext
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // 👈 permite sessão em HTTP
});

// Serviços e repositórios personalizados
builder.Services.AddScoped<SessaoUtilizadorService>();
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<IDetalheExperienciaService, DetalheExperienciaService>();
builder.Services.AddScoped<PropostaTrabalhoRepository>();
builder.Services.AddScoped<PropostaTrabalhoService>();

// Cliente HTTP
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5072")
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TalentosIT API",
        Version = "v1",
        Description = "API para gestão de talentos IT"
    });
});

var app = builder.Build();

// Ambiente dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalentosIT API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sessão e autorização
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
