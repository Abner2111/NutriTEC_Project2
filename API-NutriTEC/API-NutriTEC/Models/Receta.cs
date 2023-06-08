using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Receta
{
    [Key]
    public string nombre { get; set; }
}