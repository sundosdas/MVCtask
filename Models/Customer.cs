using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

public partial class Customer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }

    public virtual ICollection<ProductCustomer> ProductCustomers { get; set; } = new List<ProductCustomer>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
