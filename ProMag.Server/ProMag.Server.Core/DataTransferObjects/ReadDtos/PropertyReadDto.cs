namespace ProMag.Server.Core.DataTransferObjects.ReadDtos;

public class PropertyReadDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int TypeId { get; set; }

    public PropertyTypeReadDto? Type { get; set; }

    //public int? MainTaskId { get; set; }

    //public int? ProjectId { get; set; }

    public string? Value { get; set; }
}

public class PropertyTypeReadDto
{
    public int Id { get; set; }

    public string? Name { get; set; }
}