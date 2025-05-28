using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentosIT.Data;
using TalentosIT.Repository;
using TalentosIT.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Usa a connection string do ficheiro appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Serviços
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
builder.Services.AddScoped<SessaoUtilizadorService>();
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<SkillService>();
builder.Services.AddScoped<IDetalheExperienciaService, DetalheExperienciaService>();

// Swagger & HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072") });
builder.Services.AddControllers();
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

// ✅ Aplica migrations na base de dados no arranque
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}

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
app.UseSession();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();

app.Run();
