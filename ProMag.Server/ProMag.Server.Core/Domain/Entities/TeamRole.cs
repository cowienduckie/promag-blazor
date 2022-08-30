using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class TeamRole : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required, Display(Name = "Team Role Name")]
    public string Name { get; set; }

    //
    public ICollection<TeamMember> Members { get; set; }
}