namespace Gcr.Construccion.API.Entities
{
    
    public class PagoManoDeObra
    {
        public int Id { get; set; }

        public int DiasTrabajados { get; set; }

        public decimal TotalPagado { get; set; }
        
        public DateTime FechaPago { get; set; }

        public int EmpleadoId { get; set; }

        public Empleado Empleado { get; set; } 
    }
}