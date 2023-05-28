using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Medida
{
    [Key]
    public int id { get; set; }
    public string fecha { get; set; }
    public int medidacintura { get; set; }
    public int porcentajegrasa { get; set; }
    public int porcentajemusculo { get; set; }
    public int medidacadera { get; set; }
    public int medidacuello { get; set; }
    public string correocliente { get; set; }
}