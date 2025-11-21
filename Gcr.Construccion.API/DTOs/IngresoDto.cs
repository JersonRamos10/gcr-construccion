namespace Gcr.Construccion.API.DTOs
{
    public class IngresoDto
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}