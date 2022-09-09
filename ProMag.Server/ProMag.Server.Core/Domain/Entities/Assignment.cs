using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Assignment : BaseEntity
{
    [Required]
    public int SubTaskId { get; set; }

    public SubTask? SubTask { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}