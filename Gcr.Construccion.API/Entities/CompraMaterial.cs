namespace Gcr.Construccion.API.Entities
{
    
    public class CompraMaterial
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCompra { get; set; }

        public decimal MontoTotal { get; set; }
        
        //clave foreanea hace la relacion entre tablas
        public int CategoriaId { get; set; }

        public CategoriaMaterial Categoria { get; set; } = null!;
    }
}