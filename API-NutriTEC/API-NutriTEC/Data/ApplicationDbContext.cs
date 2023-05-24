using API_NutriTEC.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NutriTEC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Administrador> administrador { get; set; }
    }
}

