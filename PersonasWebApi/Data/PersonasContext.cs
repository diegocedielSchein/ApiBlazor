using Microsoft.EntityFrameworkCore;
using PersonasWebApi.Models;
namespace PersonasWebApi.Data
{
    public class PersonasContext : DbContext
    {
        public PersonasContext(DbContextOptions<PersonasContext> options) : base(options)
        {

        }
        public DbSet<Persona> Persona { get; set; }
    }
}
