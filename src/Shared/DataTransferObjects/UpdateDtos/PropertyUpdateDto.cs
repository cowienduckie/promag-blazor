namespace ProMag.Shared.DataTransferObjects.UpdateDtos;

public class PropertyUpdateDto
{
    public string? Name { get; set; }

    public int TypeId { get; set; }

    public int? MainTaskId { get; set; }

    public int? ProjectId { get; set; }

    public string? Value { get; set; }
}