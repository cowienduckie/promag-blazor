using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProMag.Server.Core.Domain.Entities;

public abstract class BaseEntity
{
    [Required] 
    public virtual bool IsDelete { get; set; } = false;

    [Required, Display(Name = "Create Time"), DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public virtual DateTime CreateTime { get; set; } = DateTime.Now;

    [Required, Display(Name = "Last Modified"), DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public virtual DateTime LastModified { get; set; } = DateTime.Now;
}