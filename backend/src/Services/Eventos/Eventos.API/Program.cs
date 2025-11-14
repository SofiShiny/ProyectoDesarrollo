using Eventos.Infraestructura.Persistencia;
using Eventos.Dominio.Repositorios;
using Eventos.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
 o.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventos API", Version = "v1" });
});

// Base de datos
builder.Services.AddDbContext<EventosDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(cfg =>
 cfg.RegisterServicesFromAssembly(typeof(Eventos.Aplicacion.AssemblyReference).Assembly));

// Repositorios
builder.Services.AddScoped<IRepositorioEvento, EventoRepository>();

// CORS
builder.Services.AddCors(options =>
{
 options.AddPolicy("AllowAll", policy =>
 {
 policy.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader();
 });
});

var app = builder.Build();

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI(c =>
{
 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventos API v1");
 c.RoutePrefix = "swagger"; // http(s)://localhost:<port>/swagger
});

app.UseCors("AllowAll");
// Evitar redirección a HTTPS si no hay endpoint https configurado
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// Health check
app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "eventos" }));

app.Run();
