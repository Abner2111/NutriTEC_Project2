using API_NutriTEC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Npgsql;


namespace API_NutriTEC.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly NpgsqlConnection con;


    public BaseController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        con = new NpgsqlConnection("Server=nutritecrelational.postgres.database.azure.com;Database=NutriTECrelational;Port=5432;User Id=nutritecadmin@nutritecrelational;Password=Nutritec1;Ssl Mode=Require;Trust Server Certificate=true;");

    }

    // Método para enviar respuestas de éxito
    protected IActionResult SuccessResponse(object data)
    {
        return Ok(data);
    }

    // Método para enviar respuestas de error
    protected IActionResult ErrorResponse(string message)
    {
        return BadRequest(new { success = false, message });
    }
    
    // Metodo para encriptar contraseña
    protected string GetMd5Hash(MD5 md5Hash, string input)
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
