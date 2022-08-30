using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Infrastructure.Configurations;
using ProMag.Server.Infrastructure.Extensions;

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
    }
}