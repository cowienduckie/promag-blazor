using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Team : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required, Display(Name = "Team Name")]
    public int Name { get; set; }

    //
    public ICollection<TeamMember> Members { get; set; }
}