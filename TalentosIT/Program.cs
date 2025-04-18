using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentosIT.Data;
using TalentosIT.Repository;
using TalentosIT.Services; // Reconhece ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços MVC e sessões
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // <-- Adiciona sessões
builder.Services.AddScoped<SessaoUtilizadorService>();
builder.Services.AddScoped<PerfilTalentoRepository>();
builder.Services.AddScoped<PerfilTalentoService>();
builder.Services.AddHttpContextAccessor(); // para o SessaoUtilizadorService funcionar


// Adicionar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adicionar controllers de API
builder.Services.AddControllers();

// Adicionar serviços do Swagger
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

builder.Services.AddHttpClient();
builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri("http://localhost:5072") });

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

app.UseSession(); // <-- Middleware de sessão (DEVE vir antes da autenticação)
app.UseAuthorization();

// Mapear controllers
app.MapControllers();

// Rotas MVC
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();