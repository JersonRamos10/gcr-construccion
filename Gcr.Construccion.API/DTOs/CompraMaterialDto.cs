namespace Gcr.Construccion.API.DTOs
{
    public class CompraMaterialDto
    {
        public int Id {get; set;}

        public string Descripcion {get; set;} = string.Empty;

        public decimal Monto {get; set;}

        public DateTime Fecha {get; set;}

        public string ProveedorNombre {get; set;} = string.Empty;

        public string CategoriaNombre {get; set;} = string.Empty;
    }
}