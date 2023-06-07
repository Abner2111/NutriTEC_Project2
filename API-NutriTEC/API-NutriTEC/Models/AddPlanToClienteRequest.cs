using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_NutriTEC.Models;

[PrimaryKey(nameof(Cliente), nameof(PlanId))]
public class AddPlanToClienteRequest
{
   
    public string Cliente { get; set; }
    
    public int PlanId { get; set; }
    public string Fecha_inicio { get; set; }
    public string Fecha_final { get; set; }
}