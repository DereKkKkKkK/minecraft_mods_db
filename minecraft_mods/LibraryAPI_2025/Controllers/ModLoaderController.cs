using BLL.Interfaces;
using DTO.ModLoader;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;


[ApiController]
[Route("modLoaders")]
public class ModLoaderController(IService<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ModLoaderDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ModLoaderDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<ModLoaderDto>> Create([FromBody] CreateModLoaderDto modLoader) => Ok(await service.Create(modLoader));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ModLoaderDto>> Update(Guid id, [FromBody] UpdateModLoaderDto modLoader)
    {
        modLoader.Id = id;
        
        return Ok(await service.Update(modLoader));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}