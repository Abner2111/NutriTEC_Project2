using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Cliente
{
    [Key]
    public string correo { get; set; }
    public string nombre { get; set; }
    public string apellido1 { get; set; }
    public string? apellido2 { get; set; }
    public string contrasena { get; set; }
    public string pais { get; set; }
    public string fecha_registro { get; set; }
    public string fecha_nacimiento { get; set; }
    public int estatura { get; set; }
    public int peso { get; set; }
}