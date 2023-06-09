using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_NutriTEC.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : BaseController
    {
        private Consumo consumo = new Consumo();
        private readonly ApplicationDbContext _context;

        public ConsumoController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// This function retrieves consumption data from a PostgreSQL database and returns it as a list
        /// of ConsumoViewModel objects.
        /// </summary>
        /// <returns>
        /// The method is returning an IActionResult object, which can be either an OkObjectResult with
        /// a list of ConsumoViewModel objects or a BadRequestObjectResult with an error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetConsumo()
        {
            string connectionString = _context.Database.GetConnectionString();
            List<ConsumoViewModel> consumoData = new List<ConsumoViewModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM VistaConsumo";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ConsumoViewModel consumoItem = new ConsumoViewModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Producto_o_Receta = reader["Producto_o_Receta"].ToString(),
                                TiempoComida = reader["TiempoComida"].ToString(),
                                Fecha = reader["Fecha"].ToString(),
                                Cliente = reader["Cliente"].ToString(),
                                Producto_Consumido = reader["Producto_Consumido"].ToString()
                            };

                            consumoData.Add(consumoItem);
                        }

                        reader.Close();
                        connection.Close();

                        return Ok(consumoData);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// These are two HTTP POST methods that register consumption of a product or recipe in a
        /// database using stored procedures.
        /// </summary>
        /// <param name="ConsumoProducto">- inptcorreo: email address of the user who consumed the
        /// product</param>
        /// <returns>
        /// The PostConsumoProducto and PostConsumoReceta methods are returning an IActionResult object.
        /// The Ok() method is called if the operation is successful and returns a 200 status code. If
        /// there is an exception, the BadRequest() method is called and returns a 400 status code along
        /// with the error message.
        /// </returns>
        [HttpPost("producto")]
        public IActionResult PostConsumoProducto(ConsumoProducto consumo_producto)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registroconsumoproducto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptcorreo", consumo_producto.inptcorreo);
            cmd.Parameters.AddWithValue("inptfecha", consumo_producto.inptfecha);
            cmd.Parameters.AddWithValue("inpttiempocomidaid", consumo_producto.inpttiempocomidaid);
            cmd.Parameters.AddWithValue("inptproductoid", consumo_producto.inptproductoid);

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

        [HttpPost("receta")]
        public IActionResult PostConsumoReceta(ConsumoReceta consumo_receta)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("udp_registroconsumoreceta", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("inptcorreo", consumo_receta.inptcorreo);
            cmd.Parameters.AddWithValue("inptfecha", consumo_receta.inptfecha);
            cmd.Parameters.AddWithValue("inpttiempocomidaid", consumo_receta.inpttiempocomidaid);
            cmd.Parameters.AddWithValue("inptrecetaname", consumo_receta.inptrecetaname);

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
