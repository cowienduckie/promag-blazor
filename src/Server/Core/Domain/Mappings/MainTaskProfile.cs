using AutoMapper;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Shared.Models;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Core.Domain.Mappings;

public class MainTaskProfile : Profile
{
    public MainTaskProfile()
    {
        // MainTask mappers
        CreateMap<MainTask, MainTaskReadDto>();
        CreateMap<MainTask, MainTaskUpdateDto>().ReverseMap();
        CreateMap<MainTaskCreateDto, MainTask>();

        // TaskStatus mappers
        CreateMap<TaskStatus, TaskStatusReadDto>();
        CreateMap<TaskStatus, SectionModel>();
    }
}