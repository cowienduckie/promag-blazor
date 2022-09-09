using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Property : BaseEntity
{
    [Required] 
    public string Name { get; set; }

    [Required]
    public int TypeId { get; set; }

    public PropertyType Type { get; set; }

    public int? MainTaskId { get; set; }

    public MainTask MainTask { get; set; }

    public int? ProjectId { get; set; }

    public Project Project { get; set; }

    public string Value { get; set; }

    public ICollection<SubProperty> SubProperties { get; set; }
}

public class PropertyType : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public ICollection<Property> Properties { get; set; }
}

public class SubProperty : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public string ColorHex { get; set; }

    [Required]
    public int PropertyId { get; set; }

    public Property Property { get; set; }
}