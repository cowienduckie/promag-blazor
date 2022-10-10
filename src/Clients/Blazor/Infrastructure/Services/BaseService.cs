using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public abstract class BaseService<TReadDto, TCreateDto, TUpdateDto> : IBaseService<TReadDto, TCreateDto, TUpdateDto>
    where TUpdateDto : class
{
    private const string _token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjNmYjc5MTQ5LTkwMWQtNDRhOC04ZTk3LTI0OTQzZGJiMzNlNCIsIm5iZiI6MTY2NTMzMzQ2MywiZXhwIjoxNjY1OTM4MjYyLCJpYXQiOjE2NjUzMzM0NjN9.CdNJz5YsOydCpL2J1TfchmjdPjQlqkEA2crjZbzHzYA";

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
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(_client.BaseAddress + uri),
            Method = HttpMethod.Get,
            Headers =
            {
                {"auth", _token}
            }
        };
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
    }

    protected async Task<TReadDto> RestPostRequest(string uri, TCreateDto createDto)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(_client.BaseAddress + uri),
            Method = HttpMethod.Post,
            Headers =
            {
                {"auth", _token}
            },
            Content = new StringContent(JsonSerializer.Serialize(createDto),
                Encoding.UTF8,
                "application/json")
        };
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return JsonSerializer.Deserialize<TReadDto>(content, _jsonSerializerOptions)
               ?? throw new InvalidOperationException();
    }

    protected async Task RestDeleteRequest(string uri)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(_client.BaseAddress + uri),
            Method = HttpMethod.Delete,
            Headers =
            {
                {"auth", _token}
            }
        };
        var response = await _client.SendAsync(request);


        if (!response.IsSuccessStatusCode) throw new ApplicationException();
    }
}