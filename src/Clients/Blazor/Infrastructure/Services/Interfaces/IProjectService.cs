using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IProjectService : IBaseService<ProjectReadDto, ProjectCreateDto, ProjectUpdateDto>
{
}