using BLL.Interfaces;
using DTO.Developer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;


[ApiController]
[Route("developers")]
public class DeveloperController(IService<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DeveloperDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DeveloperDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<DeveloperDto>> Create([FromBody] CreateDeveloperDto developer) => Ok(await service.Create(developer));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<DeveloperDto>> Update(Guid id, [FromBody] UpdateDeveloperDto developer)
    {
        developer.Id = id;
        
        return Ok(await service.Update(developer));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}