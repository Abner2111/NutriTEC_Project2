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

        // POST: api/producto/agregar
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

        // POST: api/producto/aprobar
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

        // DELETE: api/producto/eliminar/{codigoBarras}
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

        // PUT: api/producto/editar/{codigoBarras}
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
        // GET: api/producto
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
