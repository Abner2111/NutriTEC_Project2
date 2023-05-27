using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Receta receta = new Receta();
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_newProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptnombre", producto.nombre);
            cmd.Parameters.AddWithValue("inptcodigo_barras", producto.codigo_barras);
            cmd.Parameters.AddWithValue("inpttamano_porcion", producto.tamano_porcion);
            cmd.Parameters.AddWithValue("inptgrasa", producto.grasa);
            cmd.Parameters.AddWithValue("inptenergia", producto.energia);
            cmd.Parameters.AddWithValue("inptproteina", producto.proteina);
            cmd.Parameters.AddWithValue("inptsodio", producto.sodio);
            cmd.Parameters.AddWithValue("inptcarbohidratos", producto.carbohidratos);
            cmd.Parameters.AddWithValue("inpthierro", producto.hierro);
            cmd.Parameters.AddWithValue("inptvitaminad", producto.vitaminad);
            cmd.Parameters.AddWithValue("inptvitaminab6", producto.vitaminab6);
            cmd.Parameters.AddWithValue("inptvitaminac", producto.vitaminac);
            cmd.Parameters.AddWithValue("inptvitaminak", producto.vitaminak);
            cmd.Parameters.AddWithValue("inptvitaminab", producto.vitaminab);
            cmd.Parameters.AddWithValue("inptvitaminab12", producto.vitaminab12);
            cmd.Parameters.AddWithValue("inptvitaminaa", producto.vitaminaa);
            cmd.Parameters.AddWithValue("inptcalcio", producto.calcio);
            
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
    }
}