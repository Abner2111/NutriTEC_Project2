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

        /// <summary>
        /// This function retrieves a list of recipes from a database and returns them as an
        /// ActionResult.
        /// </summary>
        /// <returns>
        /// An HTTP response containing an ActionResult object that may contain an IEnumerable of Receta
        /// objects or a status code and error message if an exception occurs.
        /// </returns>
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
        /// <summary>
        /// This function retrieves a list of products used in recipes from a database and returns it as
        /// an HTTP response.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult object. If the try block is successful, it returns
        /// an Ok object with a list of products related to recipes. If there is an exception, it
        /// returns a StatusCode 500 with an error message.
        /// </returns>
        
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

        /// <summary>
        /// This function adds a product to a recipe in a database using a stored procedure.
        /// </summary>
        /// <param name="ProductoReceta">It is a class that contains two properties: "receta_name" and
        /// "producto". These properties are used to pass the name of a recipe and the name of a product
        /// to the "udpasignarproductosareceta" stored procedure.</param>
        /// <returns>
        /// The method is returning an ActionResult of type Receta (recipe) as a Task. However, in this
        /// specific case, it is returning an Ok result if the execution is successful or a BadRequest
        /// result with an error message if an exception is caught.
        /// </returns>
        
        [HttpPost("/AgregarProductos")]
        public async Task<ActionResult<Receta>> PostRecetaProductos(ProductoReceta productoreceta)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udpasignarproductosareceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("receta_", productoreceta.receta_name);
            cmd.Parameters.AddWithValue("producto_", productoreceta.producto);

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

        /// <summary>
        /// This is a C# function that creates a new recipe in a database using a stored procedure.
        /// </summary>
        /// <param name="Receta">A model or class representing a recipe, which likely contains
        /// properties such as the recipe name, ingredients, instructions, and possibly other
        /// details.</param>
        /// <returns>
        /// If the code runs successfully, an Ok result is returned. If there is an exception, a
        /// BadRequest result with the exception message is returned.
        /// </returns>
        
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

        

        /// <summary>
        /// This function deletes a product from a recipe using a stored procedure in C#.
        /// </summary>
        /// <param name="receta">a string representing the name or identifier of a recipe</param>
        /// <param name="producto">The parameter "producto" is an integer that represents the ID of a
        /// product that is being deleted from a recipe.</param>
        /// <returns>
        /// The method is returning an ActionResult of type Receta (recipe), but in this case it is
        /// returning Ok() which is a shorthand for returning a 200 status code with no content.
        /// </returns>
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
