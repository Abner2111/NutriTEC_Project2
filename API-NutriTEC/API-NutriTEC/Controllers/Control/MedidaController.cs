using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : BaseController
    {
        private Medida medida = new Medida();
        private readonly ApplicationDbContext _context;

        public MedidaController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// This function retrieves a list of Medida objects from a database and returns them as an
        /// ActionResult.
        /// </summary>
        /// <returns>
        /// An HTTP response containing a list of Medida objects retrieved from the medida table in the
        /// _dbContext database. If an exception occurs, a 500 status code and an error message are
        /// returned.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medida>>> GetMedidas()
        {
            try
            {
                var result = _dbContext.medida.ToList();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        
        /// <summary>
        /// This function retrieves a list of measurements for a specific user based on their email
        /// address.
        /// </summary>
        /// <param name="String">A data type in C# that represents a sequence of characters. In this
        /// case, it is used to represent the email address of a user.</param>
        /// <returns>
        /// An `ActionResult` that contains an `IEnumerable` of `Medida` objects filtered by the
        /// `correo` parameter. If the operation is successful, the `result` variable will contain the
        /// filtered list of `Medida` objects, which will be returned by the method. If an exception
        /// occurs, a `StatusCode` of 500 and an error message will be returned.
        /// </returns>
        [HttpGet("{correo}")]
        public async Task<ActionResult<IEnumerable<Medida>>> GetMedidasUsuario(String correo)
        {
            try
            {
                var result = _dbContext.medida.OrderByDescending(me => me.fecha).Where(m =>m.correocliente == correo).ToList();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e.Message);
            }
        }
        /// <summary>
        /// This function receives a POST request with a Medida object and stores it in a PostgreSQL
        /// database using a stored procedure.
        /// </summary>
        /// <param name="Medida">A class that contains the properties for a measurement, including fecha
        /// (date), medidacintura (waist measurement), porcentajegrasa (body fat percentage),
        /// porcentajemusculo (muscle percentage), medidacadera (hip measurement), medidacuello</param>
        /// <returns>
        /// The method is returning an IActionResult. If the execution of the stored procedure is
        /// successful, it returns an Ok() response. If there is an exception, it returns a BadRequest
        /// response with the error message.
        /// </returns>
        [HttpPost]
        public IActionResult PostMedida(Medida medida)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registromedidas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptfecha", medida.fecha);
            cmd.Parameters.AddWithValue("inptmedidacintura", medida.medidacintura);
            cmd.Parameters.AddWithValue("inptporcentajegrasa", medida.porcentajegrasa);
            cmd.Parameters.AddWithValue("inptporcentajemusculo", medida.porcentajemusculo);
            cmd.Parameters.AddWithValue("inptmedidacadera", medida.medidacadera);
            cmd.Parameters.AddWithValue("inptmedidacuello", medida.medidacuello);
            cmd.Parameters.AddWithValue("inptcorreocliente", medida.correocliente);

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