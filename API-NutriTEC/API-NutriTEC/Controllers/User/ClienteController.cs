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
        
        /// <summary>
        /// This function retrieves a list of clients from a SQL database using a stored procedure and
        /// returns a success response with the result.
        /// </summary>
        /// <returns>
        /// The method `GetClientes()` is returning an `IActionResult` object, which contains the result
        /// of a SQL query executed on the `_dbContext` object. The query selects all columns from a
        /// stored procedure called `GetCliente()`, and the result is returned as a list. The
        /// `SuccessResponse()` method is used to wrap the result in a standardized response format.
        /// </returns>
        [HttpGet]
        public IActionResult GetClientes()
        {
            var result = _dbContext.cliente.FromSqlRaw("SELECT * FROM GetCliente();").ToList();
            return SuccessResponse(result);
        }
        
        /// <summary>
        /// This function retrieves a list of clients from a database based on their email address.
        /// </summary>
        /// <param name="correo">correo is a string parameter that represents the email address of a
        /// client. This parameter is used in a SQL query to retrieve a list of clients from a database
        /// based on their email address.</param>
        /// <returns>
        /// The method is returning an IActionResult object, which contains the result of a SQL query to
        /// retrieve a list of clients based on a given email address. The result is obtained using the
        /// FromSqlInterpolated method and is returned as a SuccessResponse.
        /// </returns>
        [HttpGet("PorCorreo/{correo}")]
        public IActionResult GetClienteByCorreo(string correo)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByCorreo({correo});").ToList();
            return SuccessResponse(result);
        }
        
        /// <summary>
        /// This function retrieves a list of clients from a database based on their name.
        /// </summary>
        /// <param name="nombre">The parameter "nombre" is a string that represents the name of a
        /// client. This method retrieves a list of clients from the database whose name matches the
        /// provided "nombre" parameter.</param>
        /// <returns>
        /// The method is returning an IActionResult, which is the result of executing a SQL query to
        /// retrieve a list of clients from a database based on a given name. The result is then wrapped
        /// in a SuccessResponse object and returned to the caller.
        /// </returns>
        [HttpGet("PorNombre/{nombre}")]
        public IActionResult GetClienteByNombre(string nombre)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByNombre({nombre});").ToList();
            return SuccessResponse(result);
        }

        /// <summary>
        /// This function retrieves a list of clients from a database based on their first and last
        /// name.
        /// </summary>
        /// <param name="nombre">A string representing the first name of a client.</param>
        /// <param name="apellido1">The parameter "apellido1" represents the first last name of a
        /// client.</param>
        /// <returns>
        /// The method is returning an IActionResult, which is the result of executing a SQL query to
        /// retrieve a list of clients based on their first and last name. The result is wrapped in a
        /// SuccessResponse object.
        /// </returns>
        
        [HttpGet("PorNombreApellido/{nombre}/{apellido1}")]
        public IActionResult GetClienteByNombreApellido(string nombre, string apellido1)
        {
            var result = _dbContext.cliente.FromSqlInterpolated($"SELECT * FROM GetClienteByNombreApellido({nombre},{apellido1});").ToList();
            return SuccessResponse(result);
        }

        /// <summary>
        /// This function handles the login process for a client by checking their email and password
        /// against a hashed password stored in a database.
        /// </summary>
        /// <param name="correo">The email address of the client trying to log in.</param>
        /// <param name="contrasena">The password entered by the user for their account.</param>
        /// <returns>
        /// The method returns an IActionResult, which can be either a BadRequest response with an error
        /// message if the login credentials are incorrect, or a SuccessResponse with the result of a
        /// SQL query that retrieves information about a client based on their email and hashed
        /// password.
        /// </returns>
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

        
        /// <summary>
        /// This function adds a new client to a database, encrypting their password using MD5.
        /// </summary>
        /// <param name="Cliente">The "Cliente" parameter is an object of a class representing a client
        /// in the system. It contains properties such as "nombre" (name), "apellido" (last name),
        /// "correo" (email), "contrasena" (password), etc. This method is used to create a new</param>
        /// <returns>
        /// If the operation is successful, an Ok result is returned. If there is an exception, a
        /// BadRequest result with the exception message is returned.
        /// </returns>
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
        
        /// <summary>
        /// This function updates a client's information in a database using their email as a unique
        /// identifier.
        /// </summary>
        /// <param name="correo">The correo parameter is a string that represents the email address of
        /// the client that needs to be updated.</param>
        /// <param name="Cliente">A model or class representing a client with properties such as name,
        /// email, password, country, date of registration, date of birth, height, and weight.</param>
        /// <returns>
        /// The method is returning an IActionResult, which can be either Ok() if the update was
        /// successful, NotFound() if the client with the given email was not found, or BadRequest() if
        /// an exception occurred during the update process.
        /// </returns>
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
        
        /// <summary>
        /// This function deletes a client from a database based on their email address.
        /// </summary>
        /// <param name="correo">correo is a string parameter that represents the email address of the
        /// client that needs to be deleted from the database.</param>
        /// <returns>
        /// This method is returning an IActionResult, which can be either Ok (200 status code),
        /// NotFound (404 status code), or BadRequest (400 status code with an error message).
        /// </returns>
        
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

        /// <summary>
        /// This function adds a plan to a client in a database using SQL parameters and returns a
        /// success message or an error message.
        /// </summary>
        /// <param name="AddPlanToClienteRequest">A request object that contains the following
        /// properties:</param>
        /// <returns>
        /// The method is returning an IActionResult object. If the method executes successfully, it
        /// returns an Ok object with a message "Plan agregado al cliente exitosamente." If there is an
        /// exception, it returns a StatusCode object with a 500 status code and an error message.
        /// </returns>
        
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
        
        /// <summary>
        /// This function retrieves a list of planes associated with a client from a SQL database and
        /// returns it as a success response or an error message.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult object. If the try block is successful, it returns
        /// a SuccessResponse with a list of planesCliente. If there is an exception, it returns a
        /// StatusCode 500 with an error message.
        /// </returns>
        
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
        
        /// <summary>
        /// This function retrieves a list of plans associated with a given email address.
        /// </summary>
        /// <param name="correo">The correo parameter is a string that represents the email address of a
        /// client.</param>
        /// <returns>
        /// The method is returning an IActionResult, which could be a SuccessResponse with a list of
        /// planesCliente or a StatusCode 500 with an error message if an exception occurs.
        /// </returns>
        
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
