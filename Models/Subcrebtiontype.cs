using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Subcrebtiontype
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public decimal? Maxnumberofbeneficiaries { get; set; }

    public string? Text1 { get; set; }
    
    public string? Image { get; set; }

    public string? Text2 { get; set; }

    [NotMapped]
    public IFormFile? subImage { get; set; }

    public virtual ICollection<Subcrebtion> Subcrebtions { get; set; } = new List<Subcrebtion>();
}
