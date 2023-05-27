using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_NutriTEC.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        [Column("Codigo_barras")]
        public string CodigoBarras { get; set; }

        [Required]
        [StringLength(10)]
        [Column("Tamano_porcion")]

        public string TamanoPorcion { get; set; }

        public bool Aprobado { get; set; }

        public int? Grasa { get; set; }
        public int? Energia { get; set; }
        public int? Proteina { get; set; }
        public int? Sodio { get; set; }
        public int? Carbohidratos { get; set; }
        public int? Hierro { get; set; }
        public int? VitaminaD { get; set; }
        public int? VitaminaB6 { get; set; }
        public int? VitaminaC { get; set; }
        public int? VitaminaK { get; set; }
        public int? VitaminaB { get; set; }
        public int? VitaminaB12 { get; set; }
        public int? VitaminaA { get; set; }
        public int? Calcio { get; set; }
    }
}