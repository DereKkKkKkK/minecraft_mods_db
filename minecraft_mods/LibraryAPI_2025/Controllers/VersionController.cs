using BLL.Interfaces;
using DAL.Interfaces;
using DTO.ModVersion;
using DTO.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;

[ApiController]
[Route("versions")]
public class VersionController(IService<ModVersionDto, CreateModVersionDto, UpdateModVersionDto> service) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<ModVersionDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet]
    public async Task<ActionResult<QueryParamsDto<ModVersionDto>>> GetByPage([FromQuery] QueryParamsDto<ModVersionDto> queryParams)
    {
        if (queryParams.PageNumber < 1 || queryParams.PageSize < 1)
        {
            return BadRequest("Page number and page size must be positive integers.");
        }

        var result = await service.GetByPage(queryParams);
        return Ok(result);
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ModVersionDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ModVersionDto>> Create([FromBody] CreateModVersionDto version) => Ok(await service.Create(version));
    
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ModVersionDto>> Update(Guid id, [FromBody] UpdateModVersionDto version)
    {
        version.Id = id;
        
        return Ok(await service.Update(version));
    }
    
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}