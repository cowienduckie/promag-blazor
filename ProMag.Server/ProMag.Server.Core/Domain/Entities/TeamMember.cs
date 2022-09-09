using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class TeamMember : BaseEntity
{
    [Required]
    public int TeamId { get; set; }

    public Team? Team { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    [Required]
    public int TeamRoleId { get; set; }

    public TeamRole? TeamRole { get; set; }
}