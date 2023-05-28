
using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Medida medida = new Medida();
        private readonly ApplicationDbContext _context;

        public MedidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Medida>> PostMedida(Medida medida)
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