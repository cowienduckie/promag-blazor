using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Client.Blazor.Infrastructure.Services;

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

    public async Task<IEnumerable<ProjectReadDto>> GetAllAsync()
    {
        var response = await _client.GetAsync("projects");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var projects = JsonSerializer.Deserialize<IEnumerable<ProjectReadDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                            ?? new List<ProjectReadDto>();

        projects.ToList().ForEach(p => Console.WriteLine(p.Name));

        return projects;
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