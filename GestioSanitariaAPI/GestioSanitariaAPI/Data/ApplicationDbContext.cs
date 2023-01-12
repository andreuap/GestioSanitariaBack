using GestioSanitariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioSanitariaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }


        
    }
}
