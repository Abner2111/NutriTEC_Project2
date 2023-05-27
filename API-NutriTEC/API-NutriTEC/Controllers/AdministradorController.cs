using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

    }
    
}