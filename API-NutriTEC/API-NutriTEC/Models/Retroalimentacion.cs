using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_NutriTEC.Models
{
    public class Retroalimentacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string PacienteId { get; set; }

        public string NutricionistaId { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentario { get; set; }
    }
}