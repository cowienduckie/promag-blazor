using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class TeamRole : BaseEntity
{
    [Required, Display(Name = "Team Role Name")]
    public string Name { get; set; }

    //
    public ICollection<TeamMember> Members { get; set; }
}