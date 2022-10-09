using ProMag.Shared.Models;

namespace ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

public interface IAuthenticationService
{
    Task<SignInResponseModel> SignIn(SignInRequestModel signInModel);
    Task SignUp(SignUpRequestModel signUpModel);
}