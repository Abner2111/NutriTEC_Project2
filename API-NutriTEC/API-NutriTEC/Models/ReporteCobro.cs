using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class ReporteCobro
{
    [Key]
    public string correo { get; set; }
    public string nombre { get; set; }
    public string apellido1 { get; set; }
    public string apellido2 { get; set; }
    public string tarjeta_credito { get; set; }
    public string descripcion { get; set; }
    public int clientes { get; set; }
    public int descuento { get; set; }
    public float monto_cobrar { get; set; }
}