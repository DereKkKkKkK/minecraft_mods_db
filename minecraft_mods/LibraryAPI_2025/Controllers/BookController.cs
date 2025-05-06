using BLL.Interfaces;
using DTO.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;

[ApiController]
[Route("books")]
public class BookController(IService<BookDto, CreateBookDto, UpdateBookDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto book) => Ok(await service.Create(book));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<BookDto>> Update(Guid id, [FromBody] UpdateBookDto book)
    {
        book.Id = id;
        
        return Ok(await service.Update(book));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}