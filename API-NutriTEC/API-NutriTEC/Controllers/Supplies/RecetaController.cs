using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers.Supplies
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : BaseController
    {

        private Receta receta = new Receta();

        public RecetaController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            try
            {
                var result = _dbContext.receta.ToList();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        [HttpGet("/ObtenerProductos")]
        public IActionResult GetProductosOfRecetas()
        {
            try
            {
                var productosReceta = _dbContext.producto_receta.FromSqlRaw("SELECT * FROM GetProductosReceta;").ToList();

                return Ok(productosReceta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        [HttpPost("/AgregarProductos")]
        public async Task<ActionResult<Receta>> PostRecetaProductos(ProductoReceta productoreceta)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udpasignarProductosAReceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("receta", productoreceta.receta_name);
            cmd.Parameters.AddWithValue("producto", productoreceta.producto);

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

        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(Receta receta)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("udp_newreceta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("receta_", receta.nombre);

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
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("deleteproductoreceta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("receta_", receta);
                cmd.Parameters.AddWithValue("producto_id_", producto);

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
