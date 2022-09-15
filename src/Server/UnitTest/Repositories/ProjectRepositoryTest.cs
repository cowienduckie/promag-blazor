using FluentAssertions;
using ProMag.Server.Core.Domain.Entities;
using Xunit;

namespace ProMag.Server.UnitTest.Repositories;

public class ProjectRepositoryTest : BaseRepositoryTest<Project>
{
    [Fact]
    public async Task GetAllProjects()
    {
        // Arrange
        var status = new ProjectStatus
        {
            Id = 1,
            Name = "Testing"
        };
        var project1 = new Project
        {
            Id = 12,
            Name = "SmartLiving",
            Description = "A smart home project",
            StatusId = 1
        };
        var project2 = new Project
        {
            Id = 123,
            Name = "ProMag",
            Description = "A project management project",
            StatusId = 1
        };

        _context.ProjectStatuses?.Add(status);
        _context.Projects?.Add(project1);
        _context.Projects?.Add(project2);
        await _context.SaveChangesAsync();

        // Act
        var projects = await _repository.GetAllAsync();

        // Assert
        projects.Count().Should().Be(2);
        projects.Should().Contain(x => x.Id == 12);
        projects.Should().Contain(x => x.Id == 123);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetProjectById(int projectId)
    {
        // Arrange
        const int statusId = 1;

        _context.ProjectStatuses?.Add(new ProjectStatus
        {
            Id = statusId,
            Name = "Testing"
        });
        _context.Projects?.Add(new Project
        {
            Id = projectId,
            Name = "ProMag",
            Description = "A project management project",
            StatusId = statusId
        });
        await _context.SaveChangesAsync();

        // Act
        var project = await _repository.GetByIdAsync(projectId);

        // Assert
        project?.Id.Should().Be(projectId);
    }
}