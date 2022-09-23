using AutoMapper;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Server.Core.Domain.Mappings;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        // Project mappers
        CreateMap<Project, ProjectReadDto>();
        CreateMap<Project, ProjectUpdateDto>().ReverseMap();
        CreateMap<ProjectCreateDto, Project>();

        // ProjectStatus mappers
        CreateMap<ProjectStatus, ProjectStatusReadDto>();
    }
}