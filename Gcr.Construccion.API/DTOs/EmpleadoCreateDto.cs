namespace Gcr.Construccion.API.DTOs
{
    public class EmpleadoCreateDto
    {
        public string Nombre {get; set; } = string.Empty;

        public decimal PagoPorDia {get;set;}
    }
}