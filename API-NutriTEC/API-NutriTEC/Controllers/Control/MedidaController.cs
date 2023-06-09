using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : BaseController
    {
        private Medida medida = new Medida();
        private readonly ApplicationDbContext _context;

        public MedidaController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medida>>> GetMedidas()
        {
            try
            {
                var result = _dbContext.medida.ToList();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        
        [HttpGet("{correo}")]
        public async Task<ActionResult<IEnumerable<Medida>>> GetMedidasUsuario(String correo)
        {
            try
            {
                var result = _dbContext.medida.OrderByDescending(me => me.fecha).Where(m =>m.correocliente == correo).ToList();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        [HttpPost]
        public IActionResult PostMedida(Medida medida)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registromedidas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptfecha", medida.fecha);
            cmd.Parameters.AddWithValue("inptmedidacintura", medida.medidacintura);
            cmd.Parameters.AddWithValue("inptporcentajegrasa", medida.porcentajegrasa);
            cmd.Parameters.AddWithValue("inptporcentajemusculo", medida.porcentajemusculo);
            cmd.Parameters.AddWithValue("inptmedidacadera", medida.medidacadera);
            cmd.Parameters.AddWithValue("inptmedidacuello", medida.medidacuello);
            cmd.Parameters.AddWithValue("inptcorreocliente", medida.correocliente);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}