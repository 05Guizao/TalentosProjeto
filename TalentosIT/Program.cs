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

// Serviço para obter informações do utilizador na sessão
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessaoUtilizadorService>();

// Repositórios e serviços para PerfilTalento
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();

// Repositórios e serviços para Skills
builder.Services.AddScoped<SkillRepository>();
builder.Services.AddScoped<SkillService>();

// Serviços para Experiência
builder.Services.AddScoped<IDetalheExperienciaService, DetalheExperienciaService>();

// Adicionar DbContext para a conexão com o PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Serviço para HttpClient, utilizado para chamadas externas
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072") });

// Adicionar serviços para controllers e endpoints
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

// Configuração do ambiente para desenvolvimento
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

// Configuração do HTTPS e arquivos estáticos
app.UseHttpsRedirection();
app.UseStaticFiles();

// Configuração de roteamento e autorização
app.UseRouting();

app.UseSession(); // Middleware de sessão
app.UseAuthorization();

// Configuração das rotas para controllers
app.MapControllers();

// Configuração da rota padrão para o MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.Run();
