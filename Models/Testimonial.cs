using System;
using System.Collections.Generic;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Testimonial
{
    public decimal Id { get; set; }

    public string? State { get; set; }

    public string? Message { get; set; }

    public DateTime? Testimonialdate { get; set; }

    public string? Extra { get; set; }

    public decimal? Useraccountid { get; set; }

    public virtual Useraccount? Useraccount { get; set; }
}
