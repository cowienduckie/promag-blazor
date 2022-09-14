using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using ProMag.Server.Api;
using Xunit;

namespace ProMag.Server.IntegrationTest.Api;

public class ProjectApiTest : IDisposable
{
    private readonly HttpClient _httpClient;

    public ProjectApiTest()
    {
        var server = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //Test services
            });

        _httpClient = server.CreateClient();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    [Theory]
    [InlineData("GET")]
    public async void ProjectsGetAllTest(string method)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Projects/");

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("GET", 1)]
    public async Task ProjectsGetTest(string method, int? id = null)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Projects/{id}");

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}