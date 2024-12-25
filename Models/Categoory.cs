using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

public partial class Categoory
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
