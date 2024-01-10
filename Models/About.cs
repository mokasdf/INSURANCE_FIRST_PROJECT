using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class About
{
    public decimal Id { get; set; }

    public string? Image1 { get; set; }

    public string? Text1 { get; set; }

    public string? Text2 { get; set; }

    public string? Text3 { get; set; }

    public string? Text4 { get; set; }

    public string? Text5 { get; set; }

    public string? Text6 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public string? Image5 { get; set; }

    public string? Image6 { get; set; }




    //for the image in the site
    [NotMapped]
    public IFormFile? siteImage1 { get; set; }
    // //for the image in the site
    [NotMapped]
    public IFormFile? siteImage2 { get; set; }
    // //for the image in the site
    [NotMapped]
    public IFormFile? siteImage3 { get; set; }
    // //for the image in the site
    [NotMapped]
    public IFormFile? siteImage4 { get; set; }
    // //for the image in the site
    [NotMapped]
    public IFormFile? siteImage5 { get; set; }
    // //for the image in the site
    [NotMapped]
    public IFormFile? siteImage6 { get; set; }
}
