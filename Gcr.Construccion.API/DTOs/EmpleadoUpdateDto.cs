namespace Gcr.Construccion.API.DTOs
{
    public class EmpleadoUpdateDto
    {
        public string Nombre {get; set; } = string.Empty;

        public decimal PagoPorDia {get;set;}
    }
}