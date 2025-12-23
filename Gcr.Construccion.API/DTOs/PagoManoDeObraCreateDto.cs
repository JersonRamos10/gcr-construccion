namespace Gcr.Construccion.API.DTOs
{
    public class PagoManoDeObraCreateDto
    {
        public int EmpleadoId { get; set; }
        public int DiasTrabajados { get; set; }   
        public DateTime FechaPago { get; set; }  
    }
}