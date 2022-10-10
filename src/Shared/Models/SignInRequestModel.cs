using System.ComponentModel.DataAnnotations;

namespace ProMag.Shared.Models;

public class SignInRequestModel
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }

    [Required] public bool RememberMe { get; set; }
}