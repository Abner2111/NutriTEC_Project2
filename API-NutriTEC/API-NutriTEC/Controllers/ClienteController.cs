using System.Data;
using System.Security.Cryptography;
using System.Text;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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