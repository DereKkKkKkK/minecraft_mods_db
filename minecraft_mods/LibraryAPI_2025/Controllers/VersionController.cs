using BLL.Interfaces;
using DTO.ModVersion;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;

[ApiController]
[Route("versions")]
public class VersionController(IService<ModVersionDto, CreateModVersionDto, UpdateModVersionDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ModVersionDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ModVersionDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<ModVersionDto>> Create([FromBody] CreateModVersionDto version) => Ok(await service.Create(version));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ModVersionDto>> Update(Guid id, [FromBody] UpdateModVersionDto version)
    {
        version.Id = id;
        
        return Ok(await service.Update(version));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}