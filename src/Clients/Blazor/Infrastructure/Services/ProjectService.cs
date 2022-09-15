using Microsoft.AspNetCore.JsonPatch;
using ProMag.Clients.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Clients.Blazor.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _client;

    public ProjectService(HttpClient client)
    {
        _client = client;
    }

    public Task CreateAsync(ProjectCreateDto createDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProjectReadDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectReadDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task PartialUpdateAsync(int id, JsonPatchDocument<ProjectUpdateDto> updatePatchDoc)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, ProjectUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}