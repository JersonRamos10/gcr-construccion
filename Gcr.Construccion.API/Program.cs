
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.Mappings;
using Microsoft.EntityFrameworkCore;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//conexion a la base de datos SQL SERVERS

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Aautomapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(IngresoProfile));

//inyeccion de dependencias de los servicios
builder.Services.AddScoped<IIngresoService, IngresoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
