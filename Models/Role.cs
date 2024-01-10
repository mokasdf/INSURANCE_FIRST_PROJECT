using System;
using System.Collections.Generic;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Extra { get; set; }

    public virtual ICollection<Useraccount> Useraccounts { get; set; } = new List<Useraccount>();
}
