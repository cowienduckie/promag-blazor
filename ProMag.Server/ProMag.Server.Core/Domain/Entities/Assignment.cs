using System.ComponentModel.DataAnnotations;

namespace ProMag.Server.Core.Domain.Entities;

public class Assignment : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SubTaskId { get; set; }

    public SubTask SubTask { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    
    public Employee Employee { get; set; }
}