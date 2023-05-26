using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_NutriTEC.Models;

public class Plan
{
    [Key] 
    public int? id { get; set; }
    public string nombre { get; set; }
    public int nutricionistid { get; set; }
}