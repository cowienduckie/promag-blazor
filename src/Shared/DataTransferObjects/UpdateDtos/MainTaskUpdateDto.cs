namespace ProMag.Shared.DataTransferObjects.UpdateDtos;

public class MainTaskUpdateDto
{
    public string? Name { get; set; }

    public int ProjectId { get; set; }

    public int? MilestoneId { get; set; }

    public int StatusId { get; set; }
}