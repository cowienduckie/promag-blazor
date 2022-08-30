using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProMag.Server.Core.Domain.Entities;

public class User : IdentityUser
{
    [Required, Display(Name = "First Name")]
    public string FirstName { get; set; } = "FirstName";

    [Required, Display(Name = "Last Name")]
    public string LastName { get; set; } = "LastName";

    [Display(Name = "Date of Birth")]
    public DateTime? DateOfBirth { get; set; }

    [Required] public bool IsDelete { get; set; } = false;

    [Required, Display(Name = "Create Time"), DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    [Required, Display(Name = "Last Modified"), DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LastModified { get; set; } = DateTime.Now;
}