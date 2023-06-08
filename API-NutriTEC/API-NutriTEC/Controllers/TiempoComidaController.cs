using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_NutriTEC.Controllers
{
    [Route("api/TiempoComida")]
    [ApiController]
    public class TiempoComidaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiempoComidaController(ApplicationDbContext context)
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

