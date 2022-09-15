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
public class PropertiesController : BaseController
{
    public PropertiesController(ISupervisor supervisor) : base(supervisor)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyReadDto>>>? GetAllAsync()
    {
        try
        {
            var result = await Supervisor.GetAllAsync<Property, PropertyReadDto>();

            return !result.Any() ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var result = await Supervisor.GetByIdAsync<Property, PropertyReadDto>(id);

            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] PropertyCreateDto? createDto)
    {
        try
        {
            if (createDto == null || !ModelState.IsValid) return BadRequest();

            var result = await Supervisor.CreateAsync<Property, PropertyCreateDto, PropertyReadDto>(createDto);

            return CreatedAtRoute(new {id = result.Id}, result);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] PropertyUpdateDto? updateDto)
    {
        try
        {
            if (updateDto == null || !ModelState.IsValid) return BadRequest();

            return await Supervisor.UpdateAsync<Property, PropertyUpdateDto>(id, updateDto)
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
        [FromBody] JsonPatchDocument<PropertyUpdateDto>? updatePatchDoc)
    {
        try
        {
            var updateDto = await Supervisor.GetByIdAsync<Property, PropertyUpdateDto>(id);
            if (updateDto == null || updatePatchDoc == null || !ModelState.IsValid) return BadRequest();

            updatePatchDoc.ApplyTo(updateDto, ModelState);

            if (!TryValidateModel(updateDto)) return ValidationProblem(ModelState);

            return await Supervisor.UpdateAsync<Property, PropertyUpdateDto>(id, updateDto)
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
            if (await Supervisor.GetByIdAsync<Property, PropertyReadDto>(id) == null) return NotFound();

            return await Supervisor.DeleteAsync<Property>(id) ? NoContent() : StatusCode(500);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}