using API_NutriTEC.Controllers;
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
            modelBuilder.Entity<PlanComida>().HasKey(p => new {p.id, p.tiempocomida, p.comida});
            modelBuilder.Entity<ClienteNutricionista>().HasKey(cn => new { cn.cliente, cn.nutricionista });
            modelBuilder.Entity<AddPlanToClienteRequest>()
                .HasKey(ptc => new { ptc.Cliente, ptc.PlanId, ptc.Fecha_inicio, ptc.Fecha_final });
        }
        
        public DbSet<Administrador> administrador { get; set; }
        public DbSet<Plan> plan { get; set; }
        public DbSet<PlanComida> plancomida { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Medida> medida { get; set; }
        public DbSet<Consumo> consumo { get; set; }
        public DbSet<Receta> receta { get; set; }
        public DbSet<Nutricionista> nutricionista { get; set; }
        public DbSet<ClienteNutricionista> clientenutricionista { get; set; }
        public  DbSet<Producto> producto { get; set; }
        public DbSet<AddPlanToClienteRequest> planes_cliente { get; set; }
        public DbSet<ReporteCobro> reporte_cobro { get; set; }
        
        public DbSet<TiempoComida> tiempo_comida { get; set; }
    }
}

