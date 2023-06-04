using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers
{
    [Route("api/administrador")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/administradores
        [HttpGet("Prueba")]
        public ActionResult<IEnumerable<Administrador>> GetAdministradores()
        {
            var administradores = _context.administrador.ToList();
            return Ok(administradores);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTables()
        {
            try
            {
                var tables = _context.Database.GetDbConnection().GetSchema("Tables");
                var tableNames = new List<string>();

                foreach (DataRow row in tables.Rows)
                {
                    var tableName = row["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }

                return Ok(tableNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
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

                _context.Database.ExecuteSqlRaw(
                    "SELECT udp_validar_credencialesA(@p_correo, @p_contrasena) AS resultado",
                    correoParam, contrasenaParam, resultadoParam);

                bool resultado = resultadoParam.Value != DBNull.Value && (bool)resultadoParam.Value;

                if (resultado)
                {
                    return Ok("Credenciales válidas.");
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