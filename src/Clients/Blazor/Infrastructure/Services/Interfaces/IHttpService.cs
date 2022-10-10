namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IHttpService
{
    Task<T> Get<T>(string uri);
    Task<T> Post<T>(string uri, object value);
    Task<bool> Post(string uri, object value);
}