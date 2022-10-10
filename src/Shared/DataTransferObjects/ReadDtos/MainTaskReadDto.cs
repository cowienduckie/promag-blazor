namespace ProMag.Shared.DataTransferObjects.ReadDtos;

public class MainTaskReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int ProjectId { get; set; }

    //public int? MilestoneId { get; set; }

    public int StatusId { get; set; }

    public TaskStatusReadDto Status { get; set; } = new();

    //public List<PropertyReadDto>? Properties { get; set; }

    //public List<SubTaskReadDto>? SubTasks { get; set; }
}

public class TaskStatusReadDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}