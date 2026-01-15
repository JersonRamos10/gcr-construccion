namespace Gcr.Construccion.API.Entities
{

    public class CompraMaterial
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCompra { get; set; }

        public decimal MontoTotal { get; set; }

        // Campo opcional para la medida del material (ej: metros, kg, etc)
        public string? Medida { get; set; }

        //clave foreanea hace la relacion entre tablas
        public int CategoriaMaterialId { get; set; }

        public CategoriaMaterial? CategoriaMaterial { get; set; }

        public int? ProveedorId { get; set; }

        public Proveedor? Proveedor { get; set; }
    }
}