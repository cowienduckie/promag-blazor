using Microsoft.AspNetCore.Components;
using ProMag.Client.Blazor.Infrastructure.Routes;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.Models;

namespace ProMag.Client.Blazor.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpService _httpService;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;

    public AuthenticationService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    )
    {
        _httpService = httpService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public SignInResponseModel? User { get; private set; }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<SignInResponseModel>("user");
    }

    public async Task SignIn(SignInRequestModel signInModel)
    {
        User = await _httpService.Post<SignInResponseModel>(AuthenticationEndpoints.SignIn, signInModel);
        await _localStorageService.SetItem("user", User);
    }

    public async Task<bool> SignUp(SignUpRequestModel signUpModel)
    {
        return await _httpService.Post(AuthenticationEndpoints.SignUp, signUpModel);
    }

    public async Task SignOut()
    {
        User = null;
        await _localStorageService.RemoveItem("user");
        _navigationManager.NavigateTo("login");
    }
}