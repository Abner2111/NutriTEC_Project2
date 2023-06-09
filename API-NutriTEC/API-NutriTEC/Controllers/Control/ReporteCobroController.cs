using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteCobroController : BaseController
    {
        private ReporteCobro reporteCobro = new ReporteCobro();

        public ReporteCobroController(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// This function retrieves a list of reporte_cobro objects from a SQL database using a raw SQL
        /// query.
        /// </summary>
        /// <returns>
        /// An `ActionResult` of type `IEnumerable<ReporteCobro>` is being returned.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteCobro>>> GetReportesCobro()
        {
            try
            {
                var result = _dbContext.reporte_cobro.FromSqlRaw("SELECT * FROM GetNutricionistaInfo").ToList();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}