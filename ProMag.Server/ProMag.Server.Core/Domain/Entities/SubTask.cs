using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class SubTask : BaseEntity
{
    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public int MainTaskId { get; set; }

    public MainTask? MainTask { get; set; }

    public ICollection<Assignment>? Assignments { get; set; }
}