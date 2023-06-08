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

        // GET: api/administrador
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
