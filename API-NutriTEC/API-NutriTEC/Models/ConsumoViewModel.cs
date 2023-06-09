namespace API_NutriTEC.Models;

public class ConsumoViewModel
{
    public int Id { get; set; }
    public string Producto_o_Receta { get; set; }
    public string TiempoComida { get; set; }
    public string Fecha { get; set; }
    public string Cliente { get; set; }
    public string Producto_Consumido { get; set; }
}
