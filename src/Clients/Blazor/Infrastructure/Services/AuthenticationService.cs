using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using System.Text.Json;
using ProMag.Shared.Models;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using ProMag.Client.Blazor.Infrastructure.Routes;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AuthenticationService(HttpClient client)
    {
        _client = client;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<SignInResponseModel> SignIn(SignInRequestModel signInModel)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress + AuthenticationEndpoints.SignIn),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(signInModel),
                    Encoding.UTF8,
                    "application/json")
            };
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

            return JsonSerializer.Deserialize<SignInResponseModel>(content, _jsonSerializerOptions)
                   ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SignUp(SignUpRequestModel signUpModel)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress + AuthenticationEndpoints.SignUp),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(signUpModel),
                    Encoding.UTF8,
                    "application/json")
            };
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode) throw new ApplicationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}