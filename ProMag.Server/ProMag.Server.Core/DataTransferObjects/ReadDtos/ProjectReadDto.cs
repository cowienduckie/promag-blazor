namespace ProMag.Server.Core.DataTransferObjects.ReadDtos;

public class ProjectReadDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ProjectStatusReadDto? Status { get; set; }

    //public List<MainTaskReadDto>? MainTasks { get; set; }
}

public class ProjectStatusReadDto
{
    public int Id { get; set; }

    public string? Name { get; set; }
}