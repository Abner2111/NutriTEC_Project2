using API_NutriTEC.Models;

namespace API_NutriTEC.Data;
using MongoDB.Driver;

public class MongoDBConnection
{
    private IMongoDatabase database;

    public MongoDBConnection()
    {
        MongoClient client = new MongoClient("mongodb+srv://joanjuz:admin@nutritec.b3ikpbx.mongodb.net/?retryWrites=true&w=majority"); // Cambia la URL si tu servidor de MongoDB no está en localhost
        database = client.GetDatabase("NutriTec"); // Cambia "nombre_de_tu_base_de_datos" por el nombre de tu base de datos
        

    }

    public IMongoCollection<MDBCliente> Clientes
    {
        get { return database.GetCollection<MDBCliente>("cliente"); } // Cambia "cliente" si tu colección tiene un nombre diferente
    }
}