using System.Data;
using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        NpgsqlConnection con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");
        //NpgsqlConnection con = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        
        private Plan plan = new Plan();
        private readonly ApplicationDbContext _context;

        public PlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<Plan>>> GetPlanes()
        {
            var result = _context.plan.FromSqlRaw($"SELECT * FROM GetPlan();").ToList();
            //return Ok();
            return result;
        }
        
        [HttpGet("{id}")]
        public async Task <ActionResult<IEnumerable<Plan>>> GetPlan(int id)
        {
            var result = _context.plan.FromSqlRaw($"SELECT * FROM GetPlanById({id});").ToList();
            return result;
        }
        /*
        [HttpGet]
        public List<Plan> Get()
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("getplan", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Plan> lstPlan = new List<Plan>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    Plan pl = new Plan();
                    pl.id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    pl.nombre = Convert.ToString(dt.Rows[i]["Nombre"]);
                    pl.nutricionistid = Convert.ToInt32(dt.Rows[i]["NutricionistId"]);
                    lstPlan.Add(pl);
                }
            }
            if (lstPlan.Count > 0)
            {
                return lstPlan;
            }
            else
            {
                return null;
            }
        }*/

        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(Plan plan)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("addplan", con);
            cmd.CommandType = CommandType.StoredProcedure;
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