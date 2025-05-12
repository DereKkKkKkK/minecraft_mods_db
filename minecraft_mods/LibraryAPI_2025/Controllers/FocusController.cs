
using BLL.Interfaces;
using DTO.Book;
using DTO.Focus;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI_2025.Controllers;



[ApiController]
[Route("focuses")]
public class FocusController(IService<FocusDto, CreateFocusDto, UpdateFocusDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<FocusDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<FocusDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<FocusDto>> Create([FromBody] CreateFocusDto focus) => Ok(await service.Create(focus));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<FocusDto>> Update(Guid id, [FromBody] UpdateFocusDto focus)
    {
        focus.Id = id;
        
        return Ok(await service.Update(focus));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}