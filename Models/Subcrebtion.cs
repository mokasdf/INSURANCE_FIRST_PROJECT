using System;
using System.Collections.Generic;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Subcrebtion
{
    public decimal Id { get; set; }

    public DateTime? Subcrebtiondate { get; set; }

    public string? State { get; set; }

    public string? Extra { get; set; }

    public decimal? Subcrebtiontypeid { get; set; }

    public decimal? Useraccountid { get; set; }

    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    public virtual Subcrebtiontype? Subcrebtiontype { get; set; }

    public virtual Useraccount? Useraccount { get; set; }
}
