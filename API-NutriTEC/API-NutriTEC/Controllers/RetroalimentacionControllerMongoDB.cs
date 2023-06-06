using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using API_NutriTEC.Data;
using MongoDB.Bson;

namespace API_NutriTEC.Controllers
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

        [HttpGet]
        public ActionResult<IEnumerable<Retroalimentacion>> GetRetroalimentaciones()
        {
            var retroalimentaciones = _retroalimentacionCollection.Find(_ => true).ToList();
            return retroalimentaciones;
        }

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