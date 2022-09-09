namespace ProMag.Server.Core.DataTransferObjects.CreateDtos;

public class PropertyCreateDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int TypeId { get; set; }

    public int? MainTaskId { get; set; }

    public int? ProjectId { get; set; }

    public string? Value { get; set; }
}