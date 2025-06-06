﻿using BLL.Interfaces;
using DAL.Interfaces;
using DTO.ModLoader;
using DTO.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI_2025.Controllers;


[ApiController]
[Route("modLoaders")]
public class ModLoaderController(IService<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto> service) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<ActionResult<List<ModLoaderDto>>> GetAll() => Ok(await service.GetAll());
    
    
    [HttpGet]
    public async Task<ActionResult<QueryParamsDto<ModLoaderDto>>> GetByPage([FromQuery]  QueryParamsDto<ModLoaderDto> queryParams)
    {
        if (queryParams.PageNumber < 1 || queryParams.PageSize < 1)
        {
            return BadRequest("Page number and page size must be positive integers.");
        }
        

        var result = await service.GetByPage(queryParams);
        return Ok(result);
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ModLoaderDto>> GetById(Guid id) => Ok(await service.GetById(id));
    
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ModLoaderDto>> Create([FromBody] CreateModLoaderDto modLoader) => Ok(await service.Create(modLoader));
    
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ModLoaderDto>> Update(Guid id, [FromBody] UpdateModLoaderDto modLoader)
    {
        modLoader.Id = id;
        
        return Ok(await service.Update(modLoader));
    }
    
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}