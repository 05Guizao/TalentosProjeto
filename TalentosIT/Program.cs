using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentosIT.Data;
using TalentosIT.Repository;
using TalentosIT.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços MVC e sessões
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Sessão e contexto do utilizador
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessaoUtilizadorService>();

// Repositórios e serviços de PerfilTalento
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();

// Repositórios e serviços de Skill
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<SkillService>();

// Serviços de Experiência
builder.Services.AddScoped<IDetalheExperienciaService, DetalheExperienciaService>();

// Adicionar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// HttpClient para chamadas internas
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072") });

// Adicionar controllers de API e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sandbox API",
        Version = "v1",
        Description = "API para a aplicação Blazor Sandbox"
    });
});

var app = builder.Build();

// Configuração de ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox API V1");
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

app.UseSession(); // Middleware de sessão
app.UseAuthorization();

// Mapear controllers e rotas MVC
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.Run();