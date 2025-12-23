namespace Gcr.Construccion.API.DTOs
{
    public class EmpleadoUpdateDto
    {
        public string NombreCompleto {get; set; } = string.Empty;

        public decimal PagoPorDia {get;set;}
    }
}