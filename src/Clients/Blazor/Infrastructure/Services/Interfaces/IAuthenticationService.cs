using ProMag.Shared.Models;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IAuthenticationService
{
    SignInResponseModel? User { get; }
    Task Initialize();
    Task SignIn(SignInRequestModel signInModel);
    Task SignOut();
}