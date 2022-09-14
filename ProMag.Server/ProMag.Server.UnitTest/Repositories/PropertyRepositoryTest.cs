using FluentAssertions;
using ProMag.Server.Core.Domain.Entities;
using Xunit;

namespace ProMag.Server.UnitTest.Repositories;

public class PropertyRepositoryTest : BaseRepositoryTest<Property>
{
    [Fact]
    public async Task GetAllProperties()
    {
        // Arrange
        var propertyType = new PropertyType
        {
            Id = 1,
            Name = "Testing"
        };
        var property1 = new Property
        {
            Id = 12,
            Name = "Property1",
            TypeId = 1,
            Value = "Value1"
        };
        var property2 = new Property
        {
            Id = 123,
            Name = "Property2",
            TypeId = 1,
            Value = "Value2"
        };

        _context.PropertyTypes.Add(propertyType);
        _context.Properties.Add(property1);
        _context.Properties.Add(property2);
        await _context.SaveChangesAsync();

        // Act
        var properties = await _repository.GetAllAsync();

        // Assert
        properties.Count().Should().Be(2);
        properties.Should().Contain(x => x.Id == 12);
        properties.Should().Contain(x => x.Id == 123);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetPropertyById(int propertyId)
    {
        // Arrange
        const int typeId = 1;

        _context.PropertyTypes.Add(new PropertyType
        {
            Id = typeId,
            Name = "Testing"
        });
        _context.Properties.Add(new Property
        {
            Id = propertyId,
            Name = "Property",
            TypeId = typeId,
            Value = "Value"
        });
        await _context.SaveChangesAsync();

        // Act
        var property = await _repository.GetByIdAsync(propertyId);

        // Assert
        property?.Id.Should().Be(propertyId);
    }
}