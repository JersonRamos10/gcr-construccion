using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }

}