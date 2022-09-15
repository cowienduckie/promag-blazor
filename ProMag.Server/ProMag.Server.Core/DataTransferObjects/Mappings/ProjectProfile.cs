using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.DataTransferObjects.Mappings;

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