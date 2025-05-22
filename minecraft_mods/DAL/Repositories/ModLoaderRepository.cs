using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.ModLoader;
using DTO.Shared;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ModLoaderRepository(ApplicationContext context) : IRepository<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto>
{
    public async Task<List<ModLoaderDto>> GetAll()
    {
        List<ModLoader> modLoaders = await context.ModLoaders.ToListAsync();
        
        
        return modLoaders.Select(modLoader => new ModLoaderDto()
        {
            Id = modLoader.Id,
            Title = modLoader.Title,
            CreatedAt = modLoader.CreatedAt,
            UpdatedAt = modLoader.UpdatedAt
        }).ToList();
    }
    
    
    public async Task<QueryParamsDto<ModLoaderDto>> GetByPage(QueryParamsDto<ModLoaderDto> queryParams)
    {
        var query = context.ModLoaders.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = tags.Select(l => new ModLoaderDto()
        {
            Id = l.Id,
            Title = l.Title,
            CreatedAt = l.CreatedAt,
            UpdatedAt = l.UpdatedAt
        }).ToList();


        return new QueryParamsDto<ModLoaderDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<ModLoaderDto> GetById(Guid id)
    {
        ModLoader? modLoader = await context.ModLoaders.FindAsync(id);

        
        if (modLoader == null)
        {
            throw new KeyNotFoundException($"Mod loader with id {id} not found");
        }
        

        return new ModLoaderDto()
        {
            Id = modLoader.Id,
            Title = modLoader.Title,
            CreatedAt = modLoader.CreatedAt,
            UpdatedAt = modLoader.UpdatedAt,
        };
    }


    public async Task<ModLoaderDto> Create(CreateModLoaderDto modLoader)
    {
        ModLoader createdModLoader = new()
        {
            Title = modLoader.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        if (string.IsNullOrWhiteSpace(modLoader.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (modLoader.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.ModLoaders.Add(createdModLoader);
        await context.SaveChangesAsync();


        return new ModLoaderDto()
        {
            Id = createdModLoader.Id,
            Title = createdModLoader.Title,
            CreatedAt = createdModLoader.CreatedAt,
            UpdatedAt = createdModLoader.UpdatedAt,
        };
    }


    public async Task<ModLoaderDto> Update(UpdateModLoaderDto modLoader)
    {
        ModLoader? updatedModLoader = await context.ModLoaders.FindAsync(modLoader.Id);
        
        
        if (updatedModLoader == null)
        {
            throw new KeyNotFoundException($"Mod loader with id {modLoader.Id} not found");
        }
        
        
        updatedModLoader.Title = modLoader.Title;
        updatedModLoader.UpdatedAt = DateTime.UtcNow;
        
        
        if (string.IsNullOrWhiteSpace(modLoader.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (modLoader.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.ModLoaders.Update(updatedModLoader);
        await context.SaveChangesAsync();


        return new ModLoaderDto()
        {
            Id = updatedModLoader.Id,
            Title = updatedModLoader.Title,
            CreatedAt = updatedModLoader.CreatedAt,
            UpdatedAt = updatedModLoader.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        ModLoader? modLoader = await context.ModLoaders.FindAsync(id);
        
        if (modLoader == null)
        {
            throw new KeyNotFoundException($"Mod loader with id {id} not found");
        }
        
        context.ModLoaders.Remove(modLoader);
        await context.SaveChangesAsync();
    }
}