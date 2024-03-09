using Microsoft.EntityFrameworkCore;
using BlazorPeople.Models;
using System.Net.Http;
namespace BlazorPeople.Data
{
    public class PersonasDbContext : DbContext
    {
        public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options)
        {

        }
        public DbSet<Persona> Persona { get; set; }

        
    }
}
