using System.Data;
using System.Security.Cryptography;
using System.Text;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers.User
{
    [Route("api/nutricionista")]
    [ApiController]
    public class NutricionistaController : BaseController
    {
        public NutricionistaController(ApplicationDbContext context) : base(context)
        {
        }
        
        [HttpGet("Asociados/{nutricionista}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetPacientesDeNutricionista(int nutricionista)
        {
            var result = _dbContext.cliente.FromSqlRaw($"SELECT * FROM GetClientesDeNutricionista({nutricionista});").ToList();
            return result;
        }
        
        [HttpPost("Asociar")]
        public async Task<ActionResult<ClienteNutricionista>> AsociarClienteNutricionista(ClienteNutricionista clientenutricionista)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("AddClienteToNutricionista", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nutricionista_", clientenutricionista.nutricionista);
            cmd.Parameters.AddWithValue("cliente_", clientenutricionista.cliente);
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }

        // POST: api/nutricionista/registrar
        [HttpPost("registrar")]
        public IActionResult RegistrarNutricionista([FromBody] Nutricionista nutricionista)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string contrasenaMD5 = GetMd5Hash(md5Hash, nutricionista.Contrasena);
                nutricionista.Contrasena = contrasenaMD5;
            }

            try
            {
                var cedulaParam = new NpgsqlParameter("p_cedula", NpgsqlDbType.Integer)
                    { Value = nutricionista.Cedula };
                var fotoParam = new NpgsqlParameter("p_foto", NpgsqlDbType.Bytea) { Value = nutricionista.Foto};
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

                _dbContext.Database.ExecuteSqlRaw(
                    "CALL RegistrarNutricionista(@p_cedula, @p_foto, @p_nombre, @p_apellido1, @p_apellido2, @p_correo, @p_fecha_nacimiento, @p_tipo_cobro, @p_codigo, @p_tarjeta_credito, @p_contrasena, @p_direccion, @p_estatura, @p_peso)",
                    cedulaParam, fotoParam, nombreParam, apellido1Param, apellido2Param, correoParam,
                    fechaNacimientoParam, tipoCobroParam, codigoParam, tarjetaCreditoParam, contrasenaParam,
                    direccionParam, estaturaParam, pesoParam);

                _dbContext.SaveChanges();

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
                    _dbContext.nutricionista.FromSqlRaw("SELECT * FROM obtenernutricionistas()").ToList();

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
                _dbContext.Database.ExecuteSqlInterpolated($"CALL EliminarNutricionista({cedula})");

                _dbContext.SaveChanges();

                return Ok("Nutricionista eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        // POST: api/nutricionista/validar-credenciales
        [HttpGet("validar/{email}/{password}")]
        public IActionResult ValidarCredenciales(string email, string password)
        {
            try
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hash).Replace("-", "").ToLower();

                var correoParam = new NpgsqlParameter("p_correo", NpgsqlDbType.Varchar)
                    { Value = email };
                var contrasenaParam = new NpgsqlParameter("p_contrasena", NpgsqlDbType.Varchar)
                    { Value = hashedPassword };

                var resultadoParam = new NpgsqlParameter("p_resultado", NpgsqlDbType.Boolean)
                    { Direction = ParameterDirection.Output };

                _dbContext.Database.ExecuteSqlRaw(
                    "SELECT udp_validar_credencialesN(@p_correo, @p_contrasena) AS resultado",
                    correoParam, contrasenaParam, resultadoParam);

                bool resultado = resultadoParam.Value != DBNull.Value && (bool)resultadoParam.Value;

                if (resultado)
                {
                    return SuccessResponse("Credenciales válidas.");
                }
                else
                {
                    return BadRequest("Credenciales inválidas.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}