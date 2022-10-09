using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.Domain.Models
{
    public class SignInResponseModel
    {
        public SignInResponseModel(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
        }

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Token { get; }
    }
}