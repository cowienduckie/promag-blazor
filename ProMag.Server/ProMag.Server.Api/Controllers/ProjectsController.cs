using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Supervisor;

namespace ProMag.Server.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("CorsPolicy")]
[ApiController]
public class ProjectsController : BaseController
{
    public ProjectsController(ISupervisor supervisor) : base(supervisor)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var rs = await Supervisor.GetAllAsync<Project, ProjectReadDto>();

        return Ok(rs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var rs = await Supervisor.GetByIdAsync<Project, ProjectReadDto>(id);

        return Ok(rs);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var rs = Supervisor.Delete<Project>(id);

        await Supervisor.SaveAsync<Project>();

        return Ok(rs);
    }
}