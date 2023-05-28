using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Consumo
{
    [Key]
    public int id { get; set; }
    public string cliente { get; set; }
    public int tiempocomidaid { get; set; }
    public string fecha { get; set; }
    
    
    public int consumo_id { get; set; }
    public int producto_id { get; set; }
    public string receta_name { get; set; }
}