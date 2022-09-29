using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Routes;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public class MainTaskService : BaseService<MainTaskReadDto, MainTaskCreateDto, MainTaskUpdateDto>, IMainTaskService
{
    public MainTaskService(HttpClient client) : base(client)
    {
    }

    public override Task<IEnumerable<MainTaskReadDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<MainTaskReadDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<MainTaskReadDto> CreateAsync(MainTaskCreateDto createDto)
    {
        try
        {
            return await RestPostRequest(MainTaskEndpoints.MainTask, createDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override Task UpdateAsync(int id, MainTaskUpdateDto updateDto)
    {
        throw new NotImplementedException();
    }

    public override Task PartialUpdateAsync(int id, JsonPatchDocument<MainTaskUpdateDto> updatePatchDoc)
    {
        throw new NotImplementedException();
    }
}