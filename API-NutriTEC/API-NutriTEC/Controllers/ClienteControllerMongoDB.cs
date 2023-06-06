using API_NutriTEC.Data;
using API_NutriTEC.Models;

namespace API_NutriTEC.Controllers;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/clientes")]
[ApiController]
public class ClienteControllerMongoDB : ControllerBase
{
    private readonly IMongoCollection<MDBCliente> _clientes;

    public ClienteControllerMongoDB(MongoDBConnection dbConnection)
    {
        _clientes = dbConnection.Clientes;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _clientes.Find(FilterDefinition<MDBCliente>.Empty).ToList();
        return Ok(result);
    }

    [HttpGet("{correo}")]
    public IActionResult Get(string correo)
    {
        var result = _clientes.Find(c => c.correo == correo).FirstOrDefault();
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(MDBCliente cliente)
    {
        _clientes.InsertOne(cliente);
        return CreatedAtAction(nameof(Get), new { correo = cliente.correo }, cliente);
    }

    [HttpPut("{correo}")]
    public IActionResult Put(string correo, MDBCliente cliente)
    {
        var existingCliente = _clientes.FindOneAndUpdate(
            Builders<MDBCliente>.Filter.Eq(c => c.correo, correo),
            Builders<MDBCliente>.Update.Set(c => c.nombre, cliente.nombre)
                .Set(c => c.apellido1, cliente.apellido1)
                .Set(c => c.apellido2, cliente.apellido2)
                .Set(c => c.contrasena, cliente.contrasena)
                .Set(c => c.pais, cliente.pais)
                .Set(c => c.fecha_registro, cliente.fecha_registro)
                .Set(c => c.fecha_nacimiento, cliente.fecha_nacimiento)
                .Set(c => c.estatura, cliente.estatura)
                .Set(c => c.peso, cliente.peso)
            );
        if (existingCliente == null)
            return NotFound();

        return Ok(existingCliente);
    }

    [HttpDelete("{correo}")]
    public IActionResult Delete(string correo)
    {
        var result = _clientes.DeleteOne(c => c.correo == correo);
        if (result.DeletedCount == 0)
            return NotFound();
        
        return NoContent();
    }
}
