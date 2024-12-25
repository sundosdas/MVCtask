using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Roless
{
    public decimal Id { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
