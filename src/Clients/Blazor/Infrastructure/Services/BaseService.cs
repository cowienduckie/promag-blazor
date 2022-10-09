using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using System.Text.Json;
using System.Text;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public abstract class BaseService<TReadDto, TCreateDto, TUpdateDto> : IBaseService<TReadDto, TCreateDto, TUpdateDto> where TUpdateDto:class
{
    protected readonly HttpClient _client;
    protected readonly JsonSerializerOptions _jsonSerializerOptions;

    protected BaseService(HttpClient client)
    {
        _client = client;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public abstract Task<IEnumerable<TReadDto>> GetAllAsync();
    public abstract Task<TReadDto> GetByIdAsync(int id);
    public abstract Task DeleteAsync(int id);
    public abstract Task<TReadDto> CreateAsync(TCreateDto createDto);
    public abstract Task UpdateAsync(int id, TUpdateDto updateDto);
    public abstract Task PartialUpdateAsync(int id, JsonPatchDocument<TUpdateDto> updatePatchDoc);

    protected async Task<T> RestGetRequest<T>(string uri)
    {
        var response = await _client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
    }

    protected async Task<TReadDto> RestPostRequest(string uri, TCreateDto createDto)
    {
        var body = JsonSerializer.Serialize(createDto);
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(uri, requestContent);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return JsonSerializer.Deserialize<TReadDto>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
    }

    protected async Task RestDeleteRequest(string uri)
    {
        var response = await _client.DeleteAsync(uri);

        if (!response.IsSuccessStatusCode) throw new ApplicationException();
    }
}