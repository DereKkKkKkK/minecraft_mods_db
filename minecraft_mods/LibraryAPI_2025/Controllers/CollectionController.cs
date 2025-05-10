using BLL.Interfaces;
using DTO.Collection;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI_2025.Controllers;



[ApiController]
[Route("collections")]
public class CollectionController(IService<CollectionDto, CreateCollectionDto, UpdateCollectionDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CollectionDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CollectionDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<CollectionDto>> Create([FromBody] CreateCollectionDto collection) => Ok(await service.Create(collection));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<CollectionDto>> Update(Guid id, [FromBody] UpdateCollectionDto collection)
    {
        collection.Id = id;
        
        return Ok(await service.Update(collection));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}