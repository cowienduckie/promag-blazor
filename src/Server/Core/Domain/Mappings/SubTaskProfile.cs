using AutoMapper;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

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