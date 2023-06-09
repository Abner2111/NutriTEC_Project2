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
        
        /// <summary>
        /// This function retrieves a list of clients associated with a specific nutritionist from a
        /// database using a SQL query.
        /// </summary>
        /// <param name="nutricionista">It is an integer parameter that represents the ID of a
        /// nutricionista (nutritionist) whose associated clients are being retrieved from the
        /// database.</param>
        /// <returns>
        /// An `ActionResult` containing an `IEnumerable` of `Cliente` objects is being returned.
        /// </returns>
        [HttpGet("Asociados/{nutricionista}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetPacientesDeNutricionista(int nutricionista)
        {
            var result = _dbContext.cliente.FromSqlRaw($"SELECT * FROM GetClientesDeNutricionista({nutricionista});").ToList();
            return result;
        }
        

        /// <summary>
        /// This function associates a client with a nutritionist in a database using a stored
        /// procedure.
        /// </summary>
        /// <param name="ClienteNutricionista">A class or model that represents the relationship between
        /// a client and a nutritionist. It likely has two properties: "nutricionista" (representing the
        /// ID or object of the nutritionist) and "cliente" (representing the ID or object of the
        /// client).</param>
        /// <returns>
        /// If the code runs successfully, an Ok result is returned. If there is an error, a BadRequest
        /// result with an error message is returned.
        /// </returns>
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
        /// <summary>
        /// This function registers a new nutritionist in a database, encrypting their password using
        /// MD5.
        /// </summary>
        /// <param name="Nutricionista">A model class representing a nutritionist with properties such
        /// as Cedula, Foto, Nombre, Apellido1, Apellido2, Correo, FechaNacimiento, TipoCobro, Codigo,
        /// TarjetaCredito, Contrasena, Direccion, Estatura, and Peso</param>
        /// <returns>
        /// The method is returning an IActionResult, which can be either an Ok result with a success
        /// message or a StatusCode 500 with an error message.
        /// </returns>
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
        /// <summary>
        /// This function retrieves a list of nutricionistas from a SQL database and returns it as an
        /// HTTP response.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult object. If the try block is successful, it returns
        /// an Ok object with a list of nutricionistas. If there is an exception, it returns a
        /// StatusCode object with a 500 status code and an error message.
        /// </returns>
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
        /// <summary>
        /// This function deletes a nutritionist from a database using their ID number.
        /// </summary>
        /// <param name="cedula">The parameter "cedula" is an integer that represents the identification
        /// number of a nutritionist that needs to be deleted from a database.</param>
        /// <returns>
        /// If the try block is successful, the method will return an Ok object with the message
        /// "Nutricionista eliminado exitosamente." If there is an exception, the method will return a
        /// StatusCode 500 object with the message "Error: " and the exception message.
        /// </returns>
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
        /// <summary>
        /// This function validates user credentials by hashing the password and checking it against the
        /// stored hashed password in the database.
        /// </summary>
        /// <param name="email">A string representing the email address of the user trying to log
        /// in.</param>
        /// <param name="password">The password parameter is a string representing the user's password
        /// that will be validated against the hashed password stored in the database.</param>
        /// <returns>
        /// The method returns an IActionResult object, which can be either a SuccessResponse with the
        /// message "Credenciales válidas." or a BadRequest with the message "Credenciales inválidas."
        /// depending on whether the email and password provided are valid or not. If an exception
        /// occurs, it returns a StatusCode 500 with an error message.
        /// </returns>
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