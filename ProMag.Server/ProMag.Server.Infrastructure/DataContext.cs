using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Infrastructure.Configurations;
using ProMag.Server.Infrastructure.Extensions;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Infrastructure;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamRole> TeamRoles { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientContact> ClientContacts { get; set; }
    public DbSet<TaskStatus> TaskStatuses { get; set; }
    public DbSet<MainTask> MainTasks { get; set; }
    public DbSet<Milestone> Milestones { get; set; }
    public DbSet<MilestoneStatus> MilestoneStatuses { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectStatus> ProjectStatuses { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<SubProperty> SubProperties { get; set; }
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet")) 
                entityType.SetTableName(tableName[6..]);
        }

        modelBuilder.ApplyAllConfigurations();
        modelBuilder.CascadeAllRelationsOnDelete();

        new UserConfiguration();
        new EmployeeConfiguration();
        new TeamConfiguration();
        new TeamRoleConfiguration();
        new TeamMemberConfiguration();
        new ClientConfiguration();
        new ClientContactConfiguration();
        new TaskStatusConfiguration();
        new MainTaskConfiguration();
        new MilestoneConfiguration();
        new MilestoneStatusConfiguration();
        new ProjectConfiguration();
        new ProjectStatusConfiguration();
        new ProjectManagerConfiguration();
        new PropertyConfiguration();
        new PropertyTypeConfiguration();
        new SubPropertyConfiguration();
        new SubTaskConfiguration();
    }
}