using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Shared.Models;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IProjectService : IBaseService<ProjectReadDto, ProjectCreateDto, ProjectUpdateDto>
{
    Task<IEnumerable<ProjectSimplifiedModel>> GetAllSimplifiedAsync();
    Task<IEnumerable<SectionModel>> GetSectionsAsync();
}