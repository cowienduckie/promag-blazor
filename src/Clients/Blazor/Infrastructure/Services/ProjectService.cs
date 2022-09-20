using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Client.Blazor.Infrastructure.Routes;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ProjectService(HttpClient client)
    {
        _client = client;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
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
        var response = await _client.GetAsync(ProjectEndpoints.Projects);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<ProjectReadDto>>(content, _jsonSerializerOptions)
               ?? new List<ProjectReadDto>();
    }

    public async Task<ProjectReadDto> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"{ProjectEndpoints.Projects}/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<ProjectReadDto>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
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