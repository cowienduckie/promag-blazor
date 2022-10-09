using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Api.Services;
using ProMag.Server.Core.Domain.Models;

namespace ProMag.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInRequestModel model)
        {
            try
            {
                var response = await _userService.SignIn(model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp(SignUpRequestModel model)
        {
            try
            {
                var succeeded = await _userService.SignUp(model);

                if (!succeeded)
                    return BadRequest(new {message = "Something went wrong!"});

                return NoContent();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        private ActionResult HandleException(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e);
        }
    }
}