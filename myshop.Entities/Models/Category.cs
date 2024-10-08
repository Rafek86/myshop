using System.ComponentModel.DataAnnotations;

namespace myshop.Entities.Models;

public class Category
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;    

}