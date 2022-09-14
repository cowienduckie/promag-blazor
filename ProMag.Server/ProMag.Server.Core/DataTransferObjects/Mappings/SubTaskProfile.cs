using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.DataTransferObjects.Mappings;

public class SubTaskProfile : Profile
{
    public SubTaskProfile()
    {
        CreateMap<SubTask, SubTaskReadDto>();
        CreateMap<SubTask, SubTaskUpdateDto>().ReverseMap();
        CreateMap<SubTaskCreateDto, SubTask>();
    }
}