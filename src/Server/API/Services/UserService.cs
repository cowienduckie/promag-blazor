using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProMag.Server.Api.Configurations;
using ProMag.Server.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProMag.Server.Core.Domain.Models;

namespace ProMag.Server.Api.Services;

public interface IUserService
    {
        Task<SignInResponseModel> SignIn(SignInRequestModel model);

        Task<bool> SignUp(SignUpRequestModel model);

        Task SignOut();

        IEnumerable<User> GetAllUsers();

        Task<User> GetUserByIdAsync(string id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(
            IOptions<AppSettings> appSettings,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResponseModel> SignIn(SignInRequestModel model)
        {
            var signInResult = await _signInManager
                .PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).ConfigureAwait(false);

            if (!signInResult.Succeeded) return null;

            var user = await _userManager.FindByNameAsync(model.UserName);

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new SignInResponseModel(user, token);
        }

        public async Task<bool> SignUp(SignUpRequestModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

            return result.Succeeded;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // helper methods

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id)}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }