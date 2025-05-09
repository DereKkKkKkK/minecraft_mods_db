﻿using BLL.Interfaces;
using DTO.Tag;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;

[ApiController]
[Route("tags")]
public class TagController(IService<TagDto, CreateTagDto, UpdateTagDto> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [HttpPost]
    public async Task<ActionResult<TagDto>> Create([FromBody] CreateTagDto tag) => Ok(await service.Create(tag));
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<TagDto>> Update(Guid id, [FromBody] UpdateTagDto tag)
    {
        tag.Id = id;
        
        return Ok(await service.Update(tag));
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}