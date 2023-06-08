using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_NutriTEC.Models;

public class AddPlanToClienteRequest
{
    [Key]
    public string Cliente { get; set; }
    public int PlanId { get; set; }
    public string Fecha_inicio { get; set; }
    public string Fecha_final { get; set; }
}