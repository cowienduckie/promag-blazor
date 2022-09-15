namespace ProMag.Server.Core.DataTransferObjects.CreateDtos;

public class MainTaskCreateDto
{
    public string? Name { get; set; }

    public int ProjectId { get; set; }

    public int? MilestoneId { get; set; }

    public int StatusId { get; set; }
}