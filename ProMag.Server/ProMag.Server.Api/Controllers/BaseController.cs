using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Supervisor;

namespace ProMag.Server.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected User? CurrentUser => (User?)HttpContext.Items["User"];
    protected readonly ISupervisor Supervisor;

    protected BaseController(ISupervisor supervisor)
    {
        Supervisor = supervisor;
    }

    protected ActionResult HandleException(Exception e)
    {
        Console.WriteLine(e);
        return StatusCode(500, e);
    }
}