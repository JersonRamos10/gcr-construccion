
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.Mappings;
using Microsoft.EntityFrameworkCore;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a la base de datos SQL SERVER
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(IngresoProfile));

// Inyección de dependencias de los servicios
builder.Services.AddScoped<IIngresoService, IngresoService>();

// Registrar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
