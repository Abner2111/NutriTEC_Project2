using System.Data;
using System.Security.Cryptography;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Receta receta = new Receta();
        private readonly ApplicationDbContext _context;

        public RecetaController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        /* //UNDER CONSTRUCTION
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            var result = _context.receta.FromSqlRaw($"SELECT * FROM GetCliente();").ToList();
            return result;
        }*/
        
        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(Receta receta)
        {
            
            NpgsqlCommand cmd = new NpgsqlCommand("udp_newreceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("receta_", receta.nombre);
            cmd.Parameters.AddWithValue("producto_id_", receta.producto_id);
            
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
        /* // Under construction
        [HttpDelete("EliminarReceta/{receta}")]
        public async Task<ActionResult<Receta>> DeleteReceta(string correo)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_deleteClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("correo_", correo);
            
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
        } */
        
        [HttpDelete("EliminarProducto/{receta}/{producto}")]
        public async Task<ActionResult<Receta>> DeleteProductoReceta(string receta, int producto)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("deleteproductoreceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("receta_", receta);
            cmd.Parameters.AddWithValue("producto_id_", producto);
            
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