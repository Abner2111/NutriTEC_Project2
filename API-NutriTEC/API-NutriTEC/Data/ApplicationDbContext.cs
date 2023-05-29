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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>().HasKey(p => new {p.id, p.tiempocomida, p.comida});
        }
        
        public DbSet<Administrador> administrador { get; set; }
        public DbSet<Plan> plan { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Medida> medida { get; set; }
        public DbSet<Consumo> consumo { get; set; }
        public DbSet<Receta> receta { get; set; }
        public DbSet<Nutricionista> nutricionista { get; set; }
        
        public  DbSet<Producto> producto { get; set; }
        public DbSet<AddPlanToClienteRequest> planes_cliente { get; set; }
    }
}

