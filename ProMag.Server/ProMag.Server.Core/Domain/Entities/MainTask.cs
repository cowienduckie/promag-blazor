using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class MainTask : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public Project Project { get; set; }

    public int? MilestoneId { get; set; }

    public Milestone Milestone { get; set; }

    [Required]
    public int StatusId { get; set; }

    public TaskStatus Status { get; set; }

    public ICollection<Property> Properties { get; set; }

    public ICollection<SubTask> SubTasks { get; set; }
}

public class TaskStatus : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public ICollection<MainTask> MainTasks { get; set; }
}