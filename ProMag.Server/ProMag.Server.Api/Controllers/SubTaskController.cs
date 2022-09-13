using System.Xml.XPath;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Supervisor;
using ProMag.Server.Infrastructure.Migrations;

namespace ProMag.Server.Api.Controllers;

[Route("api/[controller]")]
[EnableCors("CorsPolicy")]
[ApiController]
public class SubTasksController : BaseController
{
    public SubTasksController(ISupervisor supervisor) : base(supervisor)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubTaskReadDto>>>? GetAllAsync()
    {
        try
        {
            var result = await Supervisor.GetAllAsync<SubTask, SubTaskReadDto>();

            return !result.Any() ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubTaskReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var result = await Supervisor.GetByIdAsync<SubTask, SubTaskReadDto>(id);

            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] SubTaskCreateDto? createDto)
    {
        try
        {
            if (createDto == null || !ModelState.IsValid) return BadRequest();

            var result = await Supervisor.CreateAsync<SubTask, SubTaskCreateDto, SubTaskReadDto>(createDto);

            return CreatedAtRoute(new {id = result.Id}, result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] SubTaskUpdateDto? updateDto)
    {
        try
        {
            if (updateDto == null || !ModelState.IsValid) return BadRequest();

            return await Supervisor.UpdateAsync<SubTask, SubTaskUpdateDto>(id, updateDto) ? NoContent() : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartialUpdateAsync(int id, [FromBody] JsonPatchDocument<SubTaskUpdateDto>? updatePatchDoc)
    {
        try
        {
            var updateDto = await Supervisor.GetByIdAsync<SubTask, SubTaskUpdateDto>(id);
            if (updateDto == null || updatePatchDoc == null || !ModelState.IsValid) return BadRequest();

            updatePatchDoc.ApplyTo(updateDto, ModelState);

            if (!TryValidateModel(updateDto))
            {
                return ValidationProblem(ModelState);
            }

            return await Supervisor.UpdateAsync<SubTask, SubTaskUpdateDto>(id, updateDto) ? NoContent() : StatusCode(500);
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
            if (await Supervisor.GetByIdAsync<SubTask, SubTaskReadDto>(id) == null) return NotFound();

            return await Supervisor.DeleteAsync<SubTask>(id) ? NoContent() : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}