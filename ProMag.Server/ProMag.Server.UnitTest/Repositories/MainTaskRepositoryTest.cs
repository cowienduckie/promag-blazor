using ProMag.Server.Core.Domain.Entities;
using Xunit;
using FluentAssertions;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.UnitTest.Repositories;

public class MainTaskRepositoryTest : BaseRepositoryTest<MainTask>
{
    [Fact]
    public async Task GetAllMainTasks()
    {
        // Arrange
        var taskStatus = new TaskStatus
        {
            Id = 1,
            Name = "Testing"
        };
        var mainTask1 = new MainTask
        {
            Id = 12,
            Name = "MainTask1",
            StatusId = 1,
        };
        var mainTask2 = new MainTask
        {
            Id = 123,
            Name = "MainTask2",
            StatusId = 1,
        };

        _context.TaskStatuses.Add(taskStatus);
        _context.MainTasks.Add(mainTask1);
        _context.MainTasks.Add(mainTask2);
        await _context.SaveChangesAsync();

        // Act
        var mainTasks = await _repository.GetAllAsync();

        // Assert
        mainTasks.Count().Should().Be(2);
        mainTasks.Should().Contain(x => x.Id == 12);
        mainTasks.Should().Contain(x => x.Id == 123);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetMainTaskById(int mainTaskId)
    {
        // Arrange
        const int statusId = 2;
        _context.TaskStatuses.Add(new TaskStatus
        {
            Id = statusId,
            Name = "Testing"
        });
        _context.MainTasks.Add(new MainTask
        {
            Id = mainTaskId,
            Name = "MainTask",
            StatusId = statusId,
        });
        await _context.SaveChangesAsync();

        // Act
        var mainTask = await _repository.GetByIdAsync(mainTaskId);

        // Assert
        mainTask?.Id.Should().Be(mainTaskId);
    }
}