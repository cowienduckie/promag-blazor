using ProMag.Server.Core.Domain.Entities;
using Xunit;
using FluentAssertions;

namespace ProMag.Server.UnitTest.Repositories;

public class SubTaskRepositoryTest : BaseRepositoryTest<SubTask>
{
    [Fact]
    public async Task GetAllSubTasks()
    {
        // Arrange
        var subTask1 = new SubTask
        {
            Id = 12,
            Name = "SmartLiving",
            Description = "A smart home subTask"
        };
        var subTask2 = new SubTask
        {
            Id = 123,
            Name = "ProMag",
            Description = "A subTask management subTask",
        };

        _context.SubTasks?.Add(subTask1);
        _context.SubTasks?.Add(subTask2);
        await _context.SaveChangesAsync();

        // Act
        var subTasks = await _repository.GetAllAsync();

        // Assert
        subTasks.Count().Should().Be(2);
        subTasks.Should().Contain(x => x.Id == 12);
        subTasks.Should().Contain(x => x.Id == 123);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetSubTaskById(int subTaskId)
    {
        // Arrange
        _context.SubTasks?.Add(new SubTask
        {
            Id = subTaskId,
            Name = "SubTask",
            Description = "SubTask Description",
        });
        await _context.SaveChangesAsync();

        // Act
        var subTask = await _repository.GetByIdAsync(subTaskId);

        // Assert
        subTask?.Id.Should().Be(subTaskId);
    }
}