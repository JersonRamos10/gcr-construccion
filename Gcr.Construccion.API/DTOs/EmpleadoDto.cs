namespace Gcr.Construccion.API.DTOs
{
    public class EmpleadoDto
    {
        public int Id {get;set;}
        public string Nombre {get; set; } = string.Empty;

        public decimal PagoPorDia {get;set;}
    }
}