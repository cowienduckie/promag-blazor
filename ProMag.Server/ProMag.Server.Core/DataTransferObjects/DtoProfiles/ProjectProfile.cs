using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.DataTransferObjects.DtoProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        // Project mappers
        CreateMap<Project, ProjectReadDto>();
        CreateMap<ProjectCreateDto, Project>();

        // ProjectStatus mappers
        CreateMap<ProjectStatus, ProjectStatusReadDto>();
    }
}