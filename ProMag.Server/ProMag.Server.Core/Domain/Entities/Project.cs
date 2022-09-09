using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Project : BaseEntity
{
    [Required, Display(Name = "Project Name")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int StatusId { get; set; }

    public ProjectStatus? Status { get; set; }

    public ICollection<Client>? Clients { get; set; }

    public List<ProjectClient>? ProjectClients { get; set; }

    public ICollection<ProjectManager>? ProjectManagers { get; set; }

    public ICollection<Milestone>? Milestones { get; set; }

    public ICollection<MainTask>? MainTasks { get; set; }

    public ICollection<Property>? DefaultProperties { get; set; }
}

public class ProjectClient
{
    public int ProjectId { get; set; }

    public Project? Project { get; set; }

    public int ClientId { get; set; }

    public Client? Client { get; set; }
}

public class ProjectStatus : BaseEntity
{
    [Required]
    public string? Name { get; set; }

    //
    public ICollection<Project>? Projects { get; set; }
}