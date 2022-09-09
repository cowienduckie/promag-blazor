using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Team : BaseEntity
{
    [Required, Display(Name = "Team Name")]
    public string? Name { get; set; }

    //
    public ICollection<TeamMember>? Members { get; set; }
}