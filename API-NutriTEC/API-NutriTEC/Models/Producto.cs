using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Producto
{
    [Key]
    public int id { get; set; }
    public string nombre { get; set; }
    public string codigo_barras { get; set; }
    public string tamano_porcion { get; set; }
    public bool aprobado { get; set; }
    public int grasa { get; set; }
    public int energia { get; set; }
    public int proteina { get; set; }
    public int sodio { get; set; }
    public int carbohidratos { get; set; }
    public int hierro { get; set; }
    public int vitaminad { get; set; }
    public int vitaminab6 { get; set; }
    public int vitaminac { get; set; }
    public int vitaminak { get; set; }
    public int vitaminab { get; set; }
    public int vitaminab12 { get; set; }
    public int vitaminaa { get; set; }
    public int calcio { get; set; }
}