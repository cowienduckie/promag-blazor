namespace ProMag.Server.Core.DataTransferObjects.CreateDtos;

public class SubTaskCreateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int MainTaskId { get; set; }
}