using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Milestone : BaseEntity
{
    [Required] public string? Name { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Deliverables { get; set; }

    public int StatusId { get; set; }

    public MilestoneStatus? Status { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; }

    public ICollection<MainTask>? MainTasks { get; set; }
}

public class MilestoneStatus : BaseEntity
{
    [Required] public string? Name { get; set; }

    public ICollection<Milestone>? Milestones { get; set; }
}