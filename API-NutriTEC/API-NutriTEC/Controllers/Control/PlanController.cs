using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : BaseController
    {
        private PlanComida _planComida = new PlanComida();
        private readonly ApplicationDbContext _context;

        public PlanController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// This function retrieves a list of plans from a database using a SQL query and returns them
        /// as an ActionResult.
        /// </summary>
        /// <returns>
        /// An `ActionResult` containing an `IEnumerable` of `Plan` objects. The `Plan` objects are
        /// retrieved from the database using a raw SQL query executed through the
        /// `_context.plan.FromSqlRaw()` method.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlanes()
        {
            var result = _context.plan.FromSqlRaw($"SELECT * FROM GetPlanes();").ToList();
            return result;
        }
        
        /// <summary>
        /// This function retrieves a list of meal plans with their associated recipes from a SQL
        /// database.
        /// </summary>
        /// <returns>
        /// An ActionResult of IEnumerable<PlanComida> is being returned.
        /// </returns>
        [HttpGet("Recetas")]
        public async Task<ActionResult<IEnumerable<PlanComida>>> GetPlanesRecetas()
        {
            var result = _context.plancomida.FromSqlRaw($"SELECT * FROM GetPlanRecetas();").ToList();
            return result;
        }
        
        /// <summary>
        /// This function retrieves a list of PlanComida objects from a SQL database using a stored
        /// procedure called GetPlanProductos().
        /// </summary>
        /// <returns>
        /// An ActionResult of IEnumerable<PlanComida> is being returned.
        /// </returns>
        [HttpGet("Productos")]
        public async Task<ActionResult<IEnumerable<PlanComida>>> GetPlanesProductos()
        {
            var result = _context.plancomida.FromSqlRaw($"SELECT * FROM GetPlanProductos();").ToList();
            return result;
        }
        
        /// <summary>
        /// This function retrieves a list of meal plans from a SQL database based on a given ID.
        /// </summary>
        /// <param name="id">The "id" parameter is an integer value that represents the unique
        /// identifier of a specific plan in the database. This method retrieves the plan with the
        /// specified "id" from the database and returns it as a list of PlanComida objects.</param>
        /// <returns>
        /// An `ActionResult` containing an `IEnumerable` of `PlanComida` objects is being returned. The
        /// `PlanComida` objects are retrieved from the database using a raw SQL query that calls a
        /// stored procedure named `GetPlanById` with the `id` parameter passed in as an argument.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PlanComida>>> GetPlan(int id)
        {
            var result = _context.plancomida.FromSqlRaw($"SELECT * FROM GetPlanById({id});").ToList();
            return result;
        }

        /// <summary>
        /// This function adds a new meal plan to a database using stored procedures and returns an HTTP
        /// response.
        /// </summary>
        /// <param name="PlanComida">A model or class that represents a meal plan, with properties such
        /// as plan (name of the plan), nutricionistid (ID of the nutritionist who created the plan),
        /// tiempocomida (meal time), and comida (food items for the meal).</param>
        /// <returns>
        /// If the try block is successful, an Ok result is returned. If there is an exception, a
        /// BadRequest result with the exception message is returned.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<PlanComida>> PostPlan(PlanComida planComida)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("addplan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nombre_", planComida.plan);
            cmd.Parameters.AddWithValue("nutricionistid_", planComida.nutricionistid);
            cmd.Parameters.AddWithValue("tiempocomida_", planComida.tiempocomida);
            cmd.Parameters.AddWithValue("comida_", planComida.comida);
            
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
        
        /// <summary>
        /// This function updates a plan with the given ID using a stored procedure in a PostgreSQL
        /// database.
        /// </summary>
        /// <param name="id">The ID of the plan that needs to be updated.</param>
        /// <param name="Plan">A class representing a nutrition plan, with properties such as "nombre"
        /// (name) and "nutricionistid" (ID of the nutritionist who created the plan).</param>
        /// <returns>
        /// The method is returning an ActionResult of type Plan. However, in this case, it is returning
        /// either an Ok result or a BadRequest result with an error message.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Plan>> PutPlan(int id, Plan plan)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("putplan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id_", id);
            cmd.Parameters.AddWithValue("nombre_", plan.nombre);
            cmd.Parameters.AddWithValue("nutricionistid_", plan.nutricionistid);
            
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
        
        /// <summary>
        /// This function deletes a plan with a specified ID from a database using a stored procedure.
        /// </summary>
        /// <param name="id">The id parameter is an integer that represents the unique identifier of the
        /// plan that needs to be deleted.</param>
        /// <returns>
        /// If the deletion is successful, an HTTP 200 OK status code is returned. If there is an error,
        /// a BadRequest status code with an error message is returned.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plan>> DeletePlan(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("deleteplan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id_", id);
            
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
