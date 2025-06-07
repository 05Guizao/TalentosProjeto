using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentosIT.Data;
using TalentosIT.Repository;
using TalentosIT.Services;

var builder = WebApplication.CreateBuilder(args);

// 📌 Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 📦 Serviços da aplicação
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

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// ✅ Injeção de Dependências
builder.Services.AddScoped<SessaoUtilizadorService>();
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<IDetalheExperienciaService, DetalheExperienciaService>();

// 🌐 Swagger
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072") });
builder.Services.AddControllers(); // necessário para endpoints API
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

// ✅ Aplicar migrações na base de dados (se necessário)
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}

// ⚙️ Middleware
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

app.UseSession(); // importante para login e estado do utilizador
app.UseAuthorization();

// Rotas MVC + API
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
