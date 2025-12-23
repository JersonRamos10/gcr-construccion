
namespace Gcr.Construccion.API.Entities
{
    public class CategoriaMaterial
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        //RELACION CON LA tabla de compras de nateriales 1 -> M
        public ICollection<CompraMaterial> CompraMateriales { get; set; } = new HashSet<CompraMaterial>();

        

    }
}