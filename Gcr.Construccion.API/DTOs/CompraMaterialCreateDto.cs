namespace Gcr.Construccion.API.DTOs
{
    public class CompraMaterialCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaCompra { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public string ProveedorNombre { get; set; } = string.Empty;

        public string? ProveedorTelefono { get; set; }
        public string? ProveedorDireccion { get; set; }
        public string? Medida { get; set; }
    }
}