using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Employee : BaseEntity
{
    [Required]
    public string UserId { get; set; }

    public User UserAccount { get; set; }

    [Required, Display(Name = "Employee Code")]
    public string Code { get; set; }

    [Required, Display(Name = "Employee Name")]
    public string Name { get; set; }

    //
    public ICollection<TeamMember> Members { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}