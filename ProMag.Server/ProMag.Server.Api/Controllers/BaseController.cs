using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Api.Controllers;


public class BaseController : ControllerBase
{
    protected User? CurrentUser => (User?) HttpContext.Items["User"];

    protected ActionResult HandleException(Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e);
    }
}