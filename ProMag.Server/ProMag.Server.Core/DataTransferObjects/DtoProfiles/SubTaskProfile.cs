using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.DataTransferObjects.DtoProfiles;

public class SubTaskProfile : Profile
{
    public SubTaskProfile()
    {
        CreateMap<SubTask, SubTaskReadDto>();
        CreateMap<SubTaskCreateDto, SubTask>();
    }
}