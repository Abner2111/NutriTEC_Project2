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
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        public ClienteController(ApplicationDbContext context) : base(context)
        {
        }
        
        [HttpGet]
        public IActionResult GetClientes()
        {
            var result = _dbContext.cliente.FromSqlRaw("SELECT * FROM GetCliente();").ToList();
            return SuccessResponse(result);
        }
        
        [HttpGet("PorCorreo/{correo}")]
        public IActionResult GetClienteByCorreo(string correo)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByCorreo({correo});").ToList();
            return SuccessResponse(result);
        }
        
        [HttpGet("PorNombre/{nombre}")]
        public IActionResult GetClienteByNombre(string nombre)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByNombre({nombre});").ToList();
            return SuccessResponse(result);
        }
        
        [HttpGet("PorNombreApellido/{nombre}/{apellido1}")]
        public IActionResult GetClienteByNombreApellido(string nombre, string apellido1)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByNombreApellido({nombre},{apellido1});").ToList();
            return SuccessResponse(result);
        }

        [HttpGet("Login/{correo}/{contrasena}")]
        public IActionResult LoginCliente(string correo, string contrasena)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
            var hashedPassword = BitConverter.ToString(hash).Replace("-", "").ToLower();

            try
            {
                var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM udp_loginClient({correo}, {hashedPassword});").ToList();
                if (result.Count == 0)
                {
                    return BadRequest("Correo o contraseña incorrectos");
                }

                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public IActionResult PostCliente(Cliente cliente)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string contrasenaMD5 = GetMd5Hash(md5Hash, cliente.contrasena);
                cliente.contrasena = contrasenaMD5;
            }
            
            try
            {
                _dbContext.cliente.Add(cliente);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{correo}")]
        public IActionResult PutCliente(string correo, Cliente cliente)
        {
            try
            {
                var existingCliente = _dbContext.cliente.FirstOrDefault(c => c.correo == correo);
                if (existingCliente == null)
                {
                    return NotFound();
                }
                
                existingCliente.nombre = cliente.nombre;
                existingCliente.apellido1 = cliente.apellido1;
                existingCliente.apellido2 = cliente.apellido2;
                existingCliente.contrasena = cliente.contrasena;
                existingCliente.pais = cliente.pais;
                existingCliente.fecha_registro = cliente.fecha_registro;
                existingCliente.fecha_nacimiento = cliente.fecha_nacimiento;
                existingCliente.estatura = cliente.estatura;
                existingCliente.peso = cliente.peso;

                _dbContext.SaveChanges();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{correo}")]
        public IActionResult DeleteCliente(string correo)
        {
            try
            {
                var existingCliente = _dbContext.cliente.FirstOrDefault(c => c.correo == correo);
                if (existingCliente == null)
                {
                    return NotFound();
                }
                
                _dbContext.cliente.Remove(existingCliente);
                _dbContext.SaveChanges();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addplan")]
        public IActionResult AddPlanToCliente([FromBody] AddPlanToClienteRequest request)
        {
            try
            {
                var clienteParam = new NpgsqlParameter("Cliente_", NpgsqlDbType.Varchar)
                    { Value = request.Cliente };
                var planIdParam = new NpgsqlParameter("PlanId_", NpgsqlDbType.Integer)
                    { Value = request.PlanId };
                var fechaInicioParam = new NpgsqlParameter("Fecha_inicio_", NpgsqlDbType.Varchar)
                    { Value = request.Fecha_inicio };
                var fechaFinalParam = new NpgsqlParameter("Fecha_final_", NpgsqlDbType.Varchar)
                    { Value = request.Fecha_final };

                _dbContext.Database.ExecuteSqlRaw(
                    "CALL AddPlanToCliente(@Cliente_, @PlanId_, @Fecha_inicio_, @Fecha_final_)",
                    clienteParam, planIdParam, fechaInicioParam, fechaFinalParam);

                _dbContext.SaveChanges();

                return Ok("Plan agregado al cliente exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        
        [HttpGet("planes")]
        public IActionResult GetPlanesOfCliente()
        {
            try
            {
                var planesCliente = _dbContext.planes_cliente.FromSqlRaw("SELECT * FROM GetPlanesCliente()").ToList();
                return SuccessResponse(planesCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        
        [HttpGet("planes/{correo}")]
        public IActionResult GetPlanesOfCliente(string correo)
        {
            try
            {
                var correoParam = new NpgsqlParameter("Correo_", NpgsqlDbType.Varchar)
                    { Value = correo };

                var planesCliente = _dbContext.planes_cliente.FromSqlRaw("SELECT * FROM GetPlanesOfCliente(@correo_)", correoParam).ToList();

                return SuccessResponse(planesCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        
        
    }
}
