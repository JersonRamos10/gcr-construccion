namespace Gcr.Construccion.API.DTOs
{
    public class CompraMaterialCreateDto
    {
        public string Descripcion {get; set;} = string.Empty;

        public decimal Monto {get; set;}

        public DateTime Fecha {get; set;}

        public int ProveedorId {get; set;} 

        public int CategoriaMaterialId {get; set;}
    }


}