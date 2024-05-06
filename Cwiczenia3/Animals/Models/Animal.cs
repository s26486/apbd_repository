using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Animals.Models;

public class Animal
{
 
    public int IdAnimal { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    
    [MaxLength(200)]
    public string Area { get; set; }
    
}