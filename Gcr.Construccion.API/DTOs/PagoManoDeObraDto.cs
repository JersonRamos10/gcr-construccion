namespace Gcr.Construccion.API.DTOs
{
    public class PagoManoDeObraDto
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string EmpleadoNombre { get; set; } = string.Empty;

        public int DiasTrabajados { get; set; }
        public decimal PagoPorDia { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime FechaPago { get; set; }
    }
}