using Gcr.Construccion.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<CategoriaMaterial> categorias { get; set; }

        public DbSet<CompraMaterial> Compras { get; set; }
        
        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<PagoManoDeObra> PagosEmpleados { get; set; }
    }

}