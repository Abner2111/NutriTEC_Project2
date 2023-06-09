using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers.Supplies
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : BaseController
    {
        public ProductoController(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// This function adds a new product to a database using the values provided in the request
        /// body.
        /// </summary>
        /// <param name="Producto">A class representing a product with the following properties:</param>
        /// <returns>
        /// The method is returning an IActionResult. The return value depends on whether the try block
        /// is successful or not. If successful, it returns a SuccessResponse with the message "Producto
        /// agregado exitosamente." If there is an exception, it returns a StatusCode 500 with the
        /// message "Error: " and the exception message.
        /// </returns>
        
        [HttpPost("agregar")]
        public IActionResult AgregarProducto([FromBody] Producto producto)
        {
            try
            {
                var nombreParam = new NpgsqlParameter("nombre", NpgsqlDbType.Varchar)
                    { Value = producto.Nombre };
                var codigoBarrasParam = new NpgsqlParameter("codigo_barras", NpgsqlDbType.Varchar)
                    { Value = producto.CodigoBarras };
                var tamanoPorcionParam = new NpgsqlParameter("tamano_porcion", NpgsqlDbType.Varchar)
                    { Value = producto.TamanoPorcion };
                var grasaParam = new NpgsqlParameter("grasa", NpgsqlDbType.Integer)
                    { Value = producto.Grasa };
                var energiaParam = new NpgsqlParameter("energia", NpgsqlDbType.Integer)
                    { Value = producto.Energia };
                var proteinaParam = new NpgsqlParameter("proteina", NpgsqlDbType.Integer)
                    { Value = producto.Proteina };
                var sodioParam = new NpgsqlParameter("sodio", NpgsqlDbType.Integer)
                    { Value = producto.Sodio };
                var carbohidratosParam = new NpgsqlParameter("carbohidratos", NpgsqlDbType.Integer)
                    { Value = producto.Carbohidratos };
                var hierroParam = new NpgsqlParameter("hierro", NpgsqlDbType.Integer)
                    { Value = producto.Hierro };
                var vitaminaDParam = new NpgsqlParameter("vitamina_d", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaD };
                var vitaminaB6Param = new NpgsqlParameter("vitamina_b6", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB6 };
                var vitaminaCParam = new NpgsqlParameter("vitamina_c", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaC };
                var vitaminaKParam = new NpgsqlParameter("vitamina_k", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaK };
                var vitaminaBParam = new NpgsqlParameter("vitamina_b", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB };
                var vitaminaB12Param = new NpgsqlParameter("vitamina_b12", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB12 };
                var vitaminaAParam = new NpgsqlParameter("vitamina_a", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaA };
                var calcioParam = new NpgsqlParameter("calcio", NpgsqlDbType.Integer)
                    { Value = producto.Calcio };

                _dbContext.Database.ExecuteSqlRaw(
                    "SELECT AgregarProducto(@nombre, @codigo_barras, @tamano_porcion, @grasa, @energia, @proteina, @sodio, @carbohidratos, @hierro, @vitamina_d, @vitamina_b6, @vitamina_c, @vitamina_k, @vitamina_b, @vitamina_b12, @vitamina_a, @calcio)",
                    nombreParam, codigoBarrasParam, tamanoPorcionParam, grasaParam, energiaParam, proteinaParam,
                    sodioParam, carbohidratosParam, hierroParam, vitaminaDParam, vitaminaB6Param, vitaminaCParam,
                    vitaminaKParam, vitaminaBParam, vitaminaB12Param, vitaminaAParam, calcioParam);

                _dbContext.SaveChanges();

                return SuccessResponse("Producto agregado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        /// <summary>
        /// This function approves a product by executing a SQL command and returns a success message or
        /// an error message if an exception occurs.
        /// </summary>
        /// <param name="productoId">an integer representing the ID of the product that needs to be
        /// approved.</param>
        /// <returns>
        /// The method is returning an IActionResult. The return value depends on whether the product is
        /// approved successfully or not. If the product is approved successfully, it returns a success
        /// response with a message "Producto aprobado exitosamente." If there is an error, it returns a
        /// status code 500 with an error message.
        /// </returns>
        
        [HttpPost("aprobar/{productoId}")]
        public IActionResult AprobarProducto( int productoId)
        {
            try
            {
                _dbContext.Database.ExecuteSqlInterpolated($"CALL AprobarProducto({productoId})");

                _dbContext.SaveChanges();

                return SuccessResponse("Producto aprobado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        /// <summary>
        /// This function deletes a product from the database by its barcode.
        /// </summary>
        /// <param name="codigoBarras">a string representing the barcode of a product that needs to be
        /// deleted from the database. This method is an HTTP DELETE endpoint that takes the barcode as
        /// a parameter and deletes the corresponding product from the database.</param>
        /// <returns>
        /// The method is returning an IActionResult. The specific type of IActionResult being returned
        /// depends on whether the try block is successful or not. If the try block is successful, the
        /// method returns a SuccessResponse with a message indicating that the product was successfully
        /// deleted. If the try block throws an exception, the method returns a StatusCode 500 with an
        /// error message.
        /// </returns>
        
        [HttpDelete("eliminar/{codigoBarras}")]
        public IActionResult EliminarProductoPorCodigoBarras(string codigoBarras)
        {
            try
            {
                _dbContext.Database.ExecuteSqlInterpolated($"CALL EliminarProductoPorCodigoBarras({codigoBarras})");

                _dbContext.SaveChanges();

                return SuccessResponse("Producto eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        /// <summary>
        /// This function edits a product in a database by its barcode using the values provided in the
        /// request body.
        /// </summary>
        /// <param name="codigoBarras">A string representing the barcode of the product to be
        /// edited.</param>
        /// <param name="Producto">A class representing a product with the following properties:</param>
        /// <returns>
        /// The method is returning an IActionResult. If the product is successfully edited, it returns
        /// a SuccessResponse with a message indicating that the product was edited successfully. If
        /// there is an error, it returns a StatusCode 500 with an error message.
        /// </returns>
        
        [HttpPut("editar/{codigoBarras}")]
        public IActionResult EditarProductoPorCodigoBarras(string codigoBarras, [FromBody] Producto producto)
        {
            try
            {
                var codigoBarrasParam = new NpgsqlParameter("codigo_barras_param", NpgsqlDbType.Varchar)
                    { Value = codigoBarras };
                var tamanoPorcionParam = new NpgsqlParameter("tamano_porcion_param", NpgsqlDbType.Varchar)
                    { Value = producto.TamanoPorcion };
                var grasaParam = new NpgsqlParameter("grasa_param", NpgsqlDbType.Integer)
                    { Value = producto.Grasa };
                var energiaParam = new NpgsqlParameter("energia_param", NpgsqlDbType.Integer)
                    { Value = producto.Energia };
                var proteinaParam = new NpgsqlParameter("proteina_param", NpgsqlDbType.Integer)
                    { Value = producto.Proteina };
                var sodioParam = new NpgsqlParameter("sodio_param", NpgsqlDbType.Integer)
                    { Value = producto.Sodio };
                var carbohidratosParam = new NpgsqlParameter("carbohidratos_param", NpgsqlDbType.Integer)
                    { Value = producto.Carbohidratos };
                var hierroParam = new NpgsqlParameter("hierro_param", NpgsqlDbType.Integer)
                    { Value = producto.Hierro };
                var vitaminaDParam = new NpgsqlParameter("vitamina_d_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaD };
                var vitaminaB6Param = new NpgsqlParameter("vitamina_b6_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB6 };
                var vitaminaCParam = new NpgsqlParameter("vitamina_c_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaC };
                var vitaminaKParam = new NpgsqlParameter("vitamina_k_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaK };
                var vitaminaBParam = new NpgsqlParameter("vitamina_b_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB };
                var vitaminaB12Param = new NpgsqlParameter("vitamina_b12_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaB12 };
                var vitaminaAParam = new NpgsqlParameter("vitamina_a_param", NpgsqlDbType.Integer)
                    { Value = producto.VitaminaA };
                var calcioParam = new NpgsqlParameter("calcio_param", NpgsqlDbType.Integer)
                    { Value = producto.Calcio };

                _dbContext.Database.ExecuteSqlRaw(
                    "CALL EditarProductoPorCodigoBarras(@codigo_barras_param, @tamano_porcion_param, @grasa_param, @energia_param, @proteina_param, @sodio_param, @carbohidratos_param, @hierro_param, @vitamina_d_param, @vitamina_b6_param, @vitamina_c_param, @vitamina_k_param, @vitamina_b_param, @vitamina_b12_param, @vitamina_a_param, @calcio_param)",
                    codigoBarrasParam, tamanoPorcionParam, grasaParam, energiaParam, proteinaParam, sodioParam,
                    carbohidratosParam, hierroParam, vitaminaDParam, vitaminaB6Param, vitaminaCParam, vitaminaKParam,
                    vitaminaBParam, vitaminaB12Param, vitaminaAParam, calcioParam);

                _dbContext.SaveChanges();

                return SuccessResponse("Producto editado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        /// <summary>
        /// This function retrieves a list of products from a SQL database and returns a success
        /// response or an error message.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult, which could be a SuccessResponse with a list of
        /// products or a StatusCode with an error message.
        /// </returns>
        
        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            try
            {
                var productos = _dbContext.producto.FromSqlRaw("SELECT * FROM ObtenerProductos()").ToList();
                return SuccessResponse(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

    }
}
