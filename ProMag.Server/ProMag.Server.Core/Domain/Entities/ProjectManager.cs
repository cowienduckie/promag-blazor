using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class ProjectManager : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    public User UserAccount { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public Project Project { get; set; }
}