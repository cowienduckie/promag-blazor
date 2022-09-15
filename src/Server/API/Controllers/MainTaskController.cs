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
public class MainTasksController : BaseController
{
    public MainTasksController(ISupervisor supervisor) : base(supervisor)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MainTaskReadDto>>>? GetAllAsync()
    {
        try
        {
            var result = await Supervisor.GetAllAsync<MainTask, MainTaskReadDto>();

            return !result.Any() ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MainTaskReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var result = await Supervisor.GetByIdAsync<MainTask, MainTaskReadDto>(id);

            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] MainTaskCreateDto? createDto)
    {
        try
        {
            if (createDto == null || !ModelState.IsValid) return BadRequest();

            var result = await Supervisor.CreateAsync<MainTask, MainTaskCreateDto, MainTaskReadDto>(createDto);

            return CreatedAtRoute(new {id = result.Id}, result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] MainTaskUpdateDto? updateDto)
    {
        try
        {
            if (updateDto == null || !ModelState.IsValid) return BadRequest();

            return await Supervisor.UpdateAsync<MainTask, MainTaskUpdateDto>(id, updateDto)
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
        [FromBody] JsonPatchDocument<MainTaskUpdateDto>? updatePatchDoc)
    {
        try
        {
            var updateDto = await Supervisor.GetByIdAsync<MainTask, MainTaskUpdateDto>(id);
            if (updateDto == null || updatePatchDoc == null || !ModelState.IsValid) return BadRequest();

            updatePatchDoc.ApplyTo(updateDto, ModelState);

            if (!TryValidateModel(updateDto)) return ValidationProblem(ModelState);

            return await Supervisor.UpdateAsync<MainTask, MainTaskUpdateDto>(id, updateDto)
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
            if (await Supervisor.GetByIdAsync<MainTask, MainTaskReadDto>(id) == null) return NotFound();

            return await Supervisor.DeleteAsync<MainTask>(id) ? NoContent() : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}