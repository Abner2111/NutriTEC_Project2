using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Receta
{
    [Key]
    public string nombre { get; set; }
    public int producto_id { get; set; }
}