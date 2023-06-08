using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/TiempoComida")]
    [ApiController]
    public class TiempoComidaController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public TiempoComidaController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        /**
         * gets all the mealtimes in the database
         */
        [HttpGet]
        public ActionResult<IEnumerable<TiempoComida>> GetTiempos_comida()
        {
            var mealTimes = _context.tiempo_comida.ToList();
            return Ok(mealTimes);
        }
    }
}

