using AutoMapper;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.Domain.Mappings;

public class SubTaskProfile : Profile
{
    public SubTaskProfile()
    {
        CreateMap<SubTask, SubTaskReadDto>();
        CreateMap<SubTask, SubTaskUpdateDto>().ReverseMap();
        CreateMap<SubTaskCreateDto, SubTask>();
    }
}