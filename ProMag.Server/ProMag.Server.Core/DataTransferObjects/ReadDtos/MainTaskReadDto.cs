using ProMag.Server.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProMag.Server.Core.DataTransferObjects.ReadDtos;

public class MainTaskReadDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ProjectId { get; set; }

    public int? MilestoneId { get; set; }

    public TaskStatusReadDto Status { get; set; }

    public List<PropertyReadDto> Properties { get; set; }

    public List<SubTaskReadDto> SubTasks { get; set; }
}

public class TaskStatusReadDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}