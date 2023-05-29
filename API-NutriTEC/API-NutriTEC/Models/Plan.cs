using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_NutriTEC.Models;

public class Plan
{
    [Key] 
    public int? id { get; set; }
    public string plan { get; set; }
    public int nutricionistid { get; set; }
    public string tiempocomida { get; set; }
    public string comida { get; set; }
}