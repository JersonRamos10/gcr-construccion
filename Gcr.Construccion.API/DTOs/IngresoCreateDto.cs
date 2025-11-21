namespace Gcr.Construccion.API.DTOs
{
    public class IngresoCreateDto
    {
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}