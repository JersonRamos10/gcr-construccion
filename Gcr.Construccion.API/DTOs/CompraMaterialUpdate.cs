namespace  Gcr.Construccion.API.DTOs
{
    public class CompraMaterialUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaCompra { get; set; }

        public int ProveedorId { get; set; }
        public int CategoriaMaterialId { get; set; }
    }
}