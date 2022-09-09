using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Supervisor;

namespace ProMag.Server.Api.Controllers;


public class BaseController : ControllerBase
{
    protected User? CurrentUser => (User?) HttpContext.Items["User"];
    protected readonly ISupervisor _supervisor;

    public BaseController(ISupervisor supervisor)
    {
        _supervisor = supervisor;
    }

    protected ActionResult HandleException(Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e);
    }
}