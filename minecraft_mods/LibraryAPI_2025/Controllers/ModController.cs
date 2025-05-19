using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Mod;
using DTO.Shared;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI_2025.Controllers;



[ApiController]
[Route("mods")]
public class ModController(IService<ModDto, CreateModDto, UpdateModDto> service) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<ModDto>>> GetAll() => Ok(await service.GetAll());


    [HttpGet]
    public async Task<ActionResult<QueryParamsDto<ModDto>>> GetByPage([FromQuery]  QueryParamsDto<ModDto> queryParams)
    {
        if (queryParams.PageNumber < 1 || queryParams.PageSize < 1)
        {
            return BadRequest("Page number and page size must be positive integers.");
        }
        

        var result = await service.GetByPage(queryParams);
        return Ok(result);
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ModDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<ModDto>> Create([FromBody] CreateModDto mod) => Ok(await service.Create(mod));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ModDto>> Update(Guid id, [FromBody] UpdateModDto mod)
    {
        mod.Id = id;
        
        return Ok(await service.Update(mod));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}