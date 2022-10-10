using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Routes;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Shared.Models;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public class ProjectService : BaseService<ProjectReadDto, ProjectCreateDto, ProjectUpdateDto>, IProjectService
{
    public ProjectService(HttpClient client) : base(client)
    {
    }

    public override async Task<ProjectReadDto> CreateAsync(ProjectCreateDto createDto)
    {
        try
        {
            return await RestPostRequest(ProjectEndpoints.Projects, createDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<ProjectReadDto>> GetAllAsync()
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

    public override async Task<ProjectReadDto> GetByIdAsync(int id)
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
            return await RestGetRequest<IEnumerable<ProjectSimplifiedModel>>(
                $"{ProjectEndpoints.Projects}?simplified=true");
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

    public override Task PartialUpdateAsync(int id, JsonPatchDocument<ProjectUpdateDto> updatePatchDoc)
    {
        throw new NotImplementedException();
    }

    public override Task UpdateAsync(int id, ProjectUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }
}