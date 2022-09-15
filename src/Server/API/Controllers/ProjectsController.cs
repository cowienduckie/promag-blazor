using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
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
    public async Task<ActionResult<IEnumerable<ProjectReadDto>>>? GetAllAsync()
    {
        try
        {
            var result = await Supervisor.GetAllAsync<Project, ProjectReadDto>();

            return !result.Any() ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var result = await Supervisor.GetByIdAsync<Project, ProjectReadDto>(id);

            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ProjectCreateDto? createDto)
    {
        try
        {
            if (createDto == null || !ModelState.IsValid) return BadRequest();

            var result = await Supervisor.CreateAsync<Project, ProjectCreateDto, ProjectReadDto>(createDto);

            return CreatedAtRoute(new {id = result.Id}, result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProjectUpdateDto? updateDto)
    {
        try
        {
            if (updateDto == null || !ModelState.IsValid) return BadRequest();

            return await Supervisor.UpdateAsync<Project, ProjectUpdateDto>(id, updateDto)
                ? NoContent()
                : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartialUpdateAsync(int id,
        [FromBody] JsonPatchDocument<ProjectUpdateDto>? updatePatchDoc)
    {
        try
        {
            var updateDto = await Supervisor.GetByIdAsync<Project, ProjectUpdateDto>(id);
            if (updateDto == null || updatePatchDoc == null || !ModelState.IsValid) return BadRequest();

            updatePatchDoc.ApplyTo(updateDto, ModelState);

            if (!TryValidateModel(updateDto)) return ValidationProblem(ModelState);

            return await Supervisor.UpdateAsync<Project, ProjectUpdateDto>(id, updateDto)
                ? NoContent()
                : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            if (await Supervisor.GetByIdAsync<Project, ProjectReadDto>(id) == null) return NotFound();

            return await Supervisor.DeleteAsync<Project>(id) ? NoContent() : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}