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