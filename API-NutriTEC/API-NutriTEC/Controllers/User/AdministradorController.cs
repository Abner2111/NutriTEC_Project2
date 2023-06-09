using System.Data;
using System.Security.Cryptography;
using System.Text;
using API_NutriTEC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers.User
{
    [Route("api/administrador")]
    [ApiController]
    public class AdministradorController : BaseController
    {
        public AdministradorController(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// This function retrieves a list of administrators from a database and returns a success
        /// response with the list if it exists, or an error response if it does not.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult. If the list of administrators is not null, it
        /// returns a SuccessResponse with the list of administrators. Otherwise, it returns an
        /// ErrorResponse with the message "No hay administradores".
        /// </returns>
        
        [HttpGet("Prueba")]
        public IActionResult GetAdministradores()
        {
            var administradores = _dbContext.administrador.ToList();
            if (administradores != null)
            {
                return SuccessResponse(administradores);

            }
            else
            {
                return ErrorResponse("No hay administradores");
            }
        }

        /// <summary>
        /// This function retrieves the names of all tables in the database and returns them as a list.
        /// </summary>
        /// <returns>
        /// The method `GetTables()` returns a list of table names in the database as a success response
        /// or an error message if an exception occurs.
        /// </returns>
        
        [HttpGet]
        public IActionResult GetTables()
        {
            try
            {
                var tables = _dbContext.Database.GetDbConnection().GetSchema("Tables");
                var tableNames = new List<string>();

                foreach (DataRow row in tables.Rows)
                {
                    var tableName = row["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }

                return SuccessResponse(tableNames);
            }
            catch (Exception ex)
            {
                return ErrorResponse("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// This function validates user credentials by hashing the password and checking it against the
        /// stored hashed password in the database.
        /// </summary>
        /// <param name="email">A string representing the email address of the user trying to log
        /// in.</param>
        /// <param name="password">The password to be validated. It is passed as a string parameter to
        /// the method.</param>
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
                    "SELECT udp_validar_credencialesA(@p_correo, @p_contrasena) AS resultado",
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
