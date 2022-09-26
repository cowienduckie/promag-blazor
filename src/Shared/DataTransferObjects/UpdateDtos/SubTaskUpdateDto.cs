namespace ProMag.Shared.DataTransferObjects.UpdateDtos;

public class SubTaskUpdateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int MainTaskId { get; set; }
}