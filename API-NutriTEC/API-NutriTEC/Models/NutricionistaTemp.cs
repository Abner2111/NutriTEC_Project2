using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace API_NutriTEC.Models;

public class NutricionistaTemp
{
    [Key]
    public int Cedula { get; set; }
    public string? Foto { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public string Correo { get; set; }

    [Column("Fecha_nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Column("Tipo_cobro")]
    public int TipoCobro { get; set; }

    public string Codigo { get; set; }

    [Column("Tarjeta_credito")]
    public string TarjetaCredito { get; set; }
    public string Contrasena { get; set; }
    public string Direccion { get; set; }
    public int Estatura { get; set; }
    public int Peso { get; set; }
}