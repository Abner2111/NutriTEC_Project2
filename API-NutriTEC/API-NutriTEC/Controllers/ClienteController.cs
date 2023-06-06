using System.Data;
using System.Security.Cryptography;
using System.Text;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Cliente cliente = new Cliente();
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var result = _context.cliente.FromSqlRaw($"SELECT * FROM GetCliente();").ToList();
            return result;
        }
        
        [HttpGet("PorCorreo/{correo}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetClienteByCorreo(string correo)
        {
            var result = _context.cliente.FromSql($"SELECT * FROM GetClienteByCorreo({correo});").ToList();
            return result;
        }
        
        [HttpGet("PorNombre/{nombre}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetClienteByNombre(string nombre)
        {
            var result = _context.cliente.FromSql($"SELECT * FROM GetClienteByNombre({nombre});").ToList();
            return result;
        }
        
        [HttpGet("PorNombreApellido/{nombre}/{apellido1}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> GetClienteByNombreApellido(string nombre, string apellido1)
        {
            var result = _context.cliente.FromSql($"SELECT * FROM GetClienteByNombreApellido({nombre},{apellido1});").ToList();
            return result;
        }

        [HttpGet("Login/{correo}/{contrasena}")]
        public async Task <ActionResult<IEnumerable<Cliente>>> LoginCliente(string correo, string contrasena)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
            var hashedPassword = BitConverter.ToString(hash).Replace("-", "").ToLower();
            
            var result = _context.cliente.FromSql($"SELECT * FROM udp_loginClient({correo}, {hashedPassword});").ToList();
            return result;
        }
        
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string contrasenaMD5 = GetMd5Hash(md5Hash, cliente.contrasena);
                cliente.contrasena = contrasenaMD5;
            }
            
            NpgsqlCommand cmd = new NpgsqlCommand("udp_newclient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("correo", cliente.correo);
            cmd.Parameters.AddWithValue("nombre", cliente.nombre);
            cmd.Parameters.AddWithValue("apellido1", cliente.apellido1);
            cmd.Parameters.AddWithValue("apellido2", cliente.apellido2);
            cmd.Parameters.AddWithValue("contrasena", cliente.contrasena);
            cmd.Parameters.AddWithValue("pais", cliente.pais);
            cmd.Parameters.AddWithValue("fecha_registro", cliente.fecha_registro);
            cmd.Parameters.AddWithValue("fecha_nacimiento", cliente.fecha_nacimiento);
            cmd.Parameters.AddWithValue("estatura", cliente.estatura);
            cmd.Parameters.AddWithValue("peso", cliente.peso);
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{correo}")]
        public async Task<ActionResult<Cliente>> PutCliente(string correo, Cliente cliente)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_updateClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("correo_", correo);
            cmd.Parameters.AddWithValue("nombre_", cliente.nombre);
            cmd.Parameters.AddWithValue("apellido1_", cliente.apellido1);
            cmd.Parameters.AddWithValue("apellido2_", cliente.apellido2);
            cmd.Parameters.AddWithValue("contrasena_", cliente.contrasena);
            cmd.Parameters.AddWithValue("pais_", cliente.pais);
            cmd.Parameters.AddWithValue("fecha_registro_", cliente.fecha_registro);
            cmd.Parameters.AddWithValue("fecha_nacimiento_", cliente.fecha_nacimiento);
            cmd.Parameters.AddWithValue("estatura_", cliente.estatura);
            cmd.Parameters.AddWithValue("peso_", cliente.peso);
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{correo}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(string correo)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_deleteClient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("correo_", correo);
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: api/cliente/addplan
        [HttpPost("addplan")]
        public IActionResult AddPlanToCliente([FromBody] AddPlanToClienteRequest request)
        {
            try
            {
                var clienteParam = new NpgsqlParameter("Cliente_", NpgsqlDbType.Varchar)
                    { Value = request.Cliente };
                var planIdParam = new NpgsqlParameter("PlanId_", NpgsqlDbType.Integer)
                    { Value = request.PlanId };
                var fechaInicioParam = new NpgsqlParameter("Fecha_inicio_", NpgsqlDbType.Date)
                    { Value = request.Fecha_inicio };
                var fechaFinalParam = new NpgsqlParameter("Fecha_final_", NpgsqlDbType.Date)
                    { Value = request.Fecha_final };

                _context.Database.ExecuteSqlRaw(
                    "CALL AddPlanToCliente(@Cliente_, @PlanId_, @Fecha_inicio_, @Fecha_final_)",
                    clienteParam, planIdParam, fechaInicioParam, fechaFinalParam);

                _context.SaveChanges();

                return Ok("Plan agregado al cliente exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        // GET: api/cliente/planes/{correo}
        [HttpGet("planes/{correo}")]
        public IActionResult GetPlanesOfCliente(string correo)
        {
            try
            {
                var correoParam = new NpgsqlParameter("Correo_", NpgsqlDbType.Varchar)
                    { Value = correo };

                var planesCliente = _context.planes_cliente.FromSqlRaw("SELECT * FROM GetPlanesOfCliente(@Correo_)", correoParam).ToList();

                return Ok(planesCliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
        
        



        
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}