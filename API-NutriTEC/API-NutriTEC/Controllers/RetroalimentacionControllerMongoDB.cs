using API_NutriTEC.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace API_NutriTEC.Controllers
{
    [ApiController]
    [Route("api/retroalimentacion")]
    public class RetroalimentacionControllerMongoDB : ControllerBase
    {
        private readonly IMongoCollection<Retroalimentacion> _retroalimentacionCollection;

        public RetroalimentacionControllerMongoDB(IMongoDatabase database)
        {
            _retroalimentacionCollection = database.GetCollection<Retroalimentacion>("retroalimentacion");
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
            retroalimentacion.Id = ObjectId.GenerateNewId().ToString();
            retroalimentacion.Fecha = DateTime.UtcNow;

            _retroalimentacionCollection.InsertOne(retroalimentacion);

            return retroalimentacion;
        }
    }
}