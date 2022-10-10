using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IMainTaskService : IBaseService<MainTaskReadDto, MainTaskCreateDto, MainTaskUpdateDto>
{
    Task<IEnumerable<MainTaskReadDto>> GetByProjectId(int projectId);
}