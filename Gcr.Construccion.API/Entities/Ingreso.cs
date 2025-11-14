namespace Gcr.Construccion.API.Entities
{
    public class Ingreso()
    {  //modelo de datos 
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

    }
}
