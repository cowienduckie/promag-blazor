using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Core.DataTransferObjects.DtoProfiles;

public class MainTaskProfile : Profile
{
    public MainTaskProfile()
    {
        // MainTask mappers
        CreateMap<MainTask, MainTaskReadDto>();
        CreateMap<MainTaskUpdateDto, MainTask>();

        // TaskStatus mappers
        CreateMap<TaskStatus, TaskStatusReadDto>();
    }
}