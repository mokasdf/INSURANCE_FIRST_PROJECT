using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Beneficiary
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Document { get; set; }
    //for the image in the site
    [NotMapped]
    public IFormFile? BeneficiaryDocument { get; set; }
    //

    public string? State { get; set; }

    public string? Relationtype { get; set; }

    public DateTime? Beneficiarystartdate { get; set; }

    public string? Extra { get; set; }

    public decimal? Subcrebtionid { get; set; }

    public virtual Subcrebtion? Subcrebtion { get; set; }
}
