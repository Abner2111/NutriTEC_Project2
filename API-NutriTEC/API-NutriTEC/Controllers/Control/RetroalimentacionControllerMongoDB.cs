using API_NutriTEC.Data;
using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API_NutriTEC.Controllers.Control
{
    [ApiController]
    [Route("api/retroalimentacion")]
    public class RetroalimentacionControllerMongoDB : ControllerBase
    {
        private readonly IMongoCollection<Retroalimentacion> _retroalimentacionCollection;

        public RetroalimentacionControllerMongoDB(MongoDBConnection dbConnection)
        {
            _retroalimentacionCollection = dbConnection.Retroalimentacion;
        }

        /// <summary>
        /// This function retrieves all the documents from a MongoDB collection named
        /// "Retroalimentacion" and returns them as a list of objects of type "Retroalimentacion".
        /// </summary>
        /// <returns>
        /// An ActionResult object containing a list of Retroalimentacion objects.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Retroalimentacion>> GetRetroalimentaciones()
        {
            var retroalimentaciones = _retroalimentacionCollection.Find(_ => true).ToList();
            return retroalimentaciones;
        }

        /// <summary>
        /// This function creates a new document in a MongoDB collection for a given Retroalimentacion
        /// object.
        /// </summary>
        /// <param name="Retroalimentacion">This is a class or model that represents a feedback or
        /// review object. It likely has properties such as a rating, comments, and other relevant
        /// information about the feedback.</param>
        /// <returns>
        /// The method is returning an instance of the `Retroalimentacion` class.
        /// </returns>
        [HttpPost]
        public ActionResult<Retroalimentacion> CreateRetroalimentacion(Retroalimentacion retroalimentacion)
        {
            retroalimentacion._id = ObjectId.GenerateNewId().ToString();
            retroalimentacion.Fecha = DateTime.UtcNow;

            _retroalimentacionCollection.InsertOne(retroalimentacion);

            return retroalimentacion;
        }
    }
}