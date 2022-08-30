using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class TeamMember : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TeamId { get; set; }

    public Team Team { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }

    [Required]
    public int TeamRoleId { get; set; }

    public TeamRole TeamRole { get; set; }

    //
    public ICollection<TeamMember> Members { get; set; }
}