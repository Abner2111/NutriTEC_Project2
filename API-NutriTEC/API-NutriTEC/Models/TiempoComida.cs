using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class TiempoComida
{
    [Key]
    public int id { set; get; }
    public string nombre { set; get; }
}