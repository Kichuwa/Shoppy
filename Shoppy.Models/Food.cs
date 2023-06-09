using System.ComponentModel.DataAnnotations;
namespace Shoppy.Models;

public class Food
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

}