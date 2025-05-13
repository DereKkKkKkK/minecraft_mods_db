using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Difficulty;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;


[ApiController]
[Route("difficulties")]
public class DifficultyController(IService<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto> service) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<DifficultyDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<DifficultyDto>>> GetByPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return BadRequest("Page number and page size must be positive integers.");
        }
        

        var result = await service.GetByPage(pageNumber, pageSize);
        return Ok(result);
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DifficultyDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<DifficultyDto>> Create([FromBody] CreateDifficultyDto difficulty) => Ok(await service.Create(difficulty));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<DifficultyDto>> Update(Guid id, [FromBody] UpdateDifficultyDto difficulty)
    {
        difficulty.Id = id;
        
        return Ok(await service.Update(difficulty));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}