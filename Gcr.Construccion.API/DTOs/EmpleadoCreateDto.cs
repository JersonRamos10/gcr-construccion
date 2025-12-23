namespace Gcr.Construccion.API.DTOs
{
    public class EmpleadoCreateDto
    {
        public string NombreCompleto {get; set; } = string.Empty;

        public decimal PagoPorDia {get;set;}
    }
}