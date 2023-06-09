using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class ProductoReceta
{
    public string receta_name { get; set; }
    public string producto { get; set; }
}