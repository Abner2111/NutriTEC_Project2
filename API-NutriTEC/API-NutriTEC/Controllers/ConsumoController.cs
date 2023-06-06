using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Consumo consumo = new Consumo();
        private readonly ApplicationDbContext _context;

        public ConsumoController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("/producto")]
        public async Task<ActionResult<Consumo>> PostConsumoProducto(ConsumoProducto consumo_producto)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registroconsumoproducto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptcorreo", consumo_producto.inptcorreo);
            cmd.Parameters.AddWithValue("inptfecha", consumo_producto.inptfecha);
            cmd.Parameters.AddWithValue("inpttiempocomidaid", consumo_producto.inpttiempocomidaid);
            cmd.Parameters.AddWithValue("inptproductoid", consumo_producto.inptproductoid);

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
        
        [HttpPost("/receta")]
        public async Task<ActionResult<Consumo>> PostConsumoReceta(ConsumoReceta consumo_receta)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registroconsumoreceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptcorreo", consumo_receta.inptcorreo);
            cmd.Parameters.AddWithValue("inptfecha", consumo_receta.inptfecha);
            cmd.Parameters.AddWithValue("inpttiempocomidaid", consumo_receta.inpttiempocomidaid);
            cmd.Parameters.AddWithValue("inptrecetaname", consumo_receta.inptrecetaname);

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