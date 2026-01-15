
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.Mappings;
using Microsoft.EntityFrameworkCore;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.Services;
using grc.Construccion.API.Mappings;
using Gcr.Construccion.API.Middlewares;

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

builder.Services.AddAutoMapper(typeof(ProveedorProfile));

// Inyección de dependencias de los servicios
builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICategoriaMaterialService, CategoriaMaterialService>();
builder.Services.AddScoped<ICompraMaterialService, CompraMaterialService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IPagoManoDeObraService, PagoManoDeObraService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Registrar controladores
builder.Services.AddControllers();


var app = builder.Build();

// Inicio de migracion automatica
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Aplica migraciones pendientes
        context.Database.Migrate();

        Console.WriteLine("Base de datos migrada correctamente.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// ... resto del código ...


//Middleware


// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
