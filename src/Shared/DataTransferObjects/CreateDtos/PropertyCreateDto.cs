namespace ProMag.Shared.DataTransferObjects.CreateDtos;

public class PropertyCreateDto
{
    public string? Name { get; set; }

    public int TypeId { get; set; }

    public int? MainTaskId { get; set; }

    public int? ProjectId { get; set; }

    public string? Value { get; set; }
}