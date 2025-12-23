namespace Gcr.Construccion.API.Entities
{
    public class Empleado{

        public int Id { get; set; }

        public string? NombreCompleto { get; set; }

        public decimal PagoPorDia { get; set; }

        public ICollection<PagoManoDeObra> PagoManoDeObras { get; set; } = new HashSet<PagoManoDeObra>();
    }
}