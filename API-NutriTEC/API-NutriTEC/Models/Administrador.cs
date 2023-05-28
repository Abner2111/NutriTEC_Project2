using System.ComponentModel.DataAnnotations;

namespace API_NutriTEC.Models;

public class Administrador
{
    [Key]
    public string correo { get; set; }

    public string contrasena { get; set; }
}