using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteCobroController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private ReporteCobro reporteCobro = new ReporteCobro();
        private readonly ApplicationDbContext _context;

        public ReporteCobroController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ReporteCobro>>> GetReportesCobro()
        {
            var result = _context.reporte_cobro.FromSqlRaw($"SELECT * FROM GetNutricionistaInfo").ToList();
            return result;
        }
    }
}