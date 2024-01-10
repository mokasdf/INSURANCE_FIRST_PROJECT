using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class Useraccount
{
    public decimal Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Image { get; set; }

    //for the image in the site
    [NotMapped]
    public IFormFile? UserImageFile { get; set; }
    //

    public string? Extra { get; set; }

    public decimal? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Subcrebtion> Subcrebtions { get; set; } = new List<Subcrebtion>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
}
