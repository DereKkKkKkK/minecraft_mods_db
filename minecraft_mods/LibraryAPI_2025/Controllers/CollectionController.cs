using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Collection;
using DTO.Shared;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI_2025.Controllers;



[ApiController]
[Route("collections")]
public class CollectionController(IService<CollectionDto, CreateCollectionDto, UpdateCollectionDto> service) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<CollectionDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet]
    public async Task<ActionResult<QueryParamsDto<CollectionDto>>> GetByPage([FromQuery] QueryParamsDto<CollectionDto> queryParams)
    {
        if (queryParams.PageNumber < 1 || queryParams.PageSize < 1)
        {
            return BadRequest("Page number and page size must be positive integers.");
        }
        

        var result = await service.GetByPage(queryParams);
        return Ok(result);
    }
    
    
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