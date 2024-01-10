using System;
using System.Collections.Generic;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Virtualbank
{
    public decimal Id { get; set; }

    public string? Ownername { get; set; }

    public decimal? Cvv { get; set; }

    public DateTime? Expirydate { get; set; }

    public decimal? Cardnumber { get; set; }

    public decimal? Balance { get; set; }

    public string? Extra { get; set; }
}
