namespace Gcr.Construccion.API.DTOs
{
    public class CompraMaterialDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public string? Medida { get; set; }

        public string ProveedorNombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
    }
}