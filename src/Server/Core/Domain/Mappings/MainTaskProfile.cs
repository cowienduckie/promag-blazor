using AutoMapper;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;
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
    }
}