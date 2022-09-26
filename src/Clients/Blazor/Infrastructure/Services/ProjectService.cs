using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Routes;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Shared.Models;

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
        try
        {
            return await RestGetRequest<IEnumerable<ProjectReadDto>>(ProjectEndpoints.Projects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<ProjectReadDto>();
        }
    }

    public async Task<ProjectReadDto> GetByIdAsync(int id)
    {
        try
        {
            return await RestGetRequest<ProjectReadDto>($"{ProjectEndpoints.Projects}/{id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ProjectReadDto();
        }
    }

    public async Task<IEnumerable<ProjectSimplifiedModel>> GetAllSimplifiedAsync()
    {
        try
        {
            return await RestGetRequest<IEnumerable<ProjectSimplifiedModel>>($"{ProjectEndpoints.Projects}?simplified=true");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<ProjectSimplifiedModel>();
        }
    }

    public async Task<IEnumerable<SectionModel>> GetSectionsAsync()
    {
        try
        {
            return await RestGetRequest<IEnumerable<SectionModel>>(ProjectEndpoints.Sections);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<SectionModel>();
        }
    }

    public Task PartialUpdateAsync(int id, JsonPatchDocument<ProjectUpdateDto> updatePatchDoc)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, ProjectUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }

    private async Task<T> RestGetRequest<T>(string uri)
    {
        var response = await _client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
    }
}