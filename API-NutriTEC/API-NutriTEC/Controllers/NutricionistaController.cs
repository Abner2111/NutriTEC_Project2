using Microsoft.AspNetCore.Mvc;
using API_NutriTEC.Models;
using System;
using System.Data;
using API_NutriTEC.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers
{
    [Route("api/nutricionista")]
    [ApiController]
    public class NutricionistaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NutricionistaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/nutricionista/registrar
        [HttpPost("registrar")]
        public IActionResult RegistrarNutricionista([FromBody] Nutricionista nutricionista)
        {
            try
            {
                var cedulaParam = new NpgsqlParameter("p_cedula", NpgsqlDbType.Integer)
                    { Value = nutricionista.Cedula };
                var fotoParam = new NpgsqlParameter("p_foto", NpgsqlDbType.Bytea) { Value = nutricionista.Foto };
                var nombreParam = new NpgsqlParameter("p_nombre", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Nombre };
                var apellido1Param = new NpgsqlParameter("p_apellido1", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Apellido1 };
                var apellido2Param = new NpgsqlParameter("p_apellido2", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Apellido2 };
                var correoParam = new NpgsqlParameter("p_correo", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Correo };
                var fechaNacimientoParam = new NpgsqlParameter("p_fecha_nacimiento", NpgsqlDbType.Date)
                    { Value = nutricionista.FechaNacimiento };
                var tipoCobroParam = new NpgsqlParameter("p_tipo_cobro", NpgsqlDbType.Integer)
                    { Value = nutricionista.TipoCobro };
                var codigoParam = new NpgsqlParameter("p_codigo", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Codigo };
                var tarjetaCreditoParam = new NpgsqlParameter("p_tarjeta_credito", NpgsqlDbType.Varchar)
                    { Value = nutricionista.TarjetaCredito };
                var contrasenaParam = new NpgsqlParameter("p_contrasena", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Contrasena };
                var direccionParam = new NpgsqlParameter("p_direccion", NpgsqlDbType.Varchar)
                    { Value = nutricionista.Direccion };
                var estaturaParam = new NpgsqlParameter("p_estatura", NpgsqlDbType.Integer)
                    { Value = nutricionista.Estatura };
                var pesoParam = new NpgsqlParameter("p_peso", NpgsqlDbType.Integer) { Value = nutricionista.Peso };

                _context.Database.ExecuteSqlRaw(
                    "CALL RegistrarNutricionista(@p_cedula, @p_foto, @p_nombre, @p_apellido1, @p_apellido2, @p_correo, @p_fecha_nacimiento, @p_tipo_cobro, @p_codigo, @p_tarjeta_credito, @p_contrasena, @p_direccion, @p_estatura, @p_peso)",
                    cedulaParam, fotoParam, nombreParam, apellido1Param, apellido2Param, correoParam,
                    fechaNacimientoParam, tipoCobroParam, codigoParam, tarjetaCreditoParam, contrasenaParam,
                    direccionParam, estaturaParam, pesoParam);

                _context.SaveChanges();

                return Ok("Nutricionista registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        // GET: api/nutricionista
        [HttpGet]
        public IActionResult ObtenerNutricionistas()
        {
            try
            {
                var nutricionistas =
                    _context.nutricionista.FromSqlRaw("SELECT * FROM obtenernutricionistas()").ToList();

                return Ok(nutricionistas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        // DELETE: api/nutricionista/eliminar/{cedula}
        [HttpDelete("eliminar/{cedula}")]
        public IActionResult EliminarNutricionista(int cedula)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"CALL EliminarNutricionista({cedula})");

                _context.SaveChanges();

                return Ok("Nutricionista eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

    }
}