using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_NutriTEC.Models;

public class AddPlanToClienteRequest
{
    [Key]
    public string Cliente { get; set; }
    public int PlanId { get; set; }
    public DateTime Fecha_inicio { get; set; }
    public DateTime Fecha_final { get; set; }
}