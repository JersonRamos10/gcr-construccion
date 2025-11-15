namespace Gcr.Construccion.API.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public int CompraMaterialId { get; set; }

        public Proveedor Proveedores { get; set; }
    }
}
