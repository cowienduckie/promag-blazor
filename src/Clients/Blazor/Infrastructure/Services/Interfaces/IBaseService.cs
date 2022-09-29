using Microsoft.AspNetCore.JsonPatch;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IBaseService<TReadDto>
{
    Task<IEnumerable<TReadDto>> GetAllAsync();
    Task<TReadDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}

public interface IBaseService<TReadDto, TCreateDto> : IBaseService<TReadDto>
{
    Task<TReadDto> CreateAsync(TCreateDto createDto);
}

public interface IBaseService<TReadDto, TCreateDto, TUpdateDto> : IBaseService<TReadDto, TCreateDto>
    where TUpdateDto : class
{
    Task UpdateAsync(int id, TUpdateDto updateDto);
    Task PartialUpdateAsync(int id, JsonPatchDocument<TUpdateDto> updatePatchDoc);
}