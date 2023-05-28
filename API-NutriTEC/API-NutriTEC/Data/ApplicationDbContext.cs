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
        public DbSet<Plan> plan { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Medida> medida { get; set; }
        public DbSet<Consumo> consumo { get; set; }
        public DbSet<Receta> receta { get; set; }
    }
}

