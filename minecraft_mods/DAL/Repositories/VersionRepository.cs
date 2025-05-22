using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.ModVersion;
using DTO.Shared;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VersionRepository(ApplicationContext context) : IRepository<ModVersionDto, CreateModVersionDto, UpdateModVersionDto>
{
    public async Task<List<ModVersionDto>> GetAll()
    {
        List<ModVersion> versions = await context.ModVersions.ToListAsync();
        
        
        return versions.Select(v => new ModVersionDto()
        {
            Id = v.Id,
            Title = v.Title,
            CreatedAt = v.CreatedAt,
            UpdatedAt = v.UpdatedAt
        }).ToList();
    }
    
    
    public async Task<QueryParamsDto<ModVersionDto>> GetByPage(QueryParamsDto<ModVersionDto> queryParams)
    {
        var query = context.ModVersions.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = tags.Select(v => new ModVersionDto()
        {
            Id = v.Id,
            Title = v.Title,
            CreatedAt = v.CreatedAt,
            UpdatedAt = v.UpdatedAt
        }).ToList();


        return new QueryParamsDto<ModVersionDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<ModVersionDto> GetById(Guid id)
    {
        ModVersion? mod_version = await context.ModVersions.FindAsync(id);

        
        if (mod_version == null)
        {
            throw new KeyNotFoundException($"Version with id {id} not found");
        }
        

        return new ModVersionDto()
        {
            Id = mod_version.Id,
            Title = mod_version.Title,
            CreatedAt = mod_version.CreatedAt,
            UpdatedAt = mod_version.UpdatedAt,
        };
    }


    public async Task<ModVersionDto> Create(CreateModVersionDto mod_version)
    {
        ModVersion createdVersion = new()
        {
            Title = mod_version.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        if (string.IsNullOrWhiteSpace(mod_version.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (mod_version.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.ModVersions.Add(createdVersion);
        await context.SaveChangesAsync();


        return new ModVersionDto()
        {
            Id = createdVersion.Id,
            Title = createdVersion.Title,
            CreatedAt = createdVersion.CreatedAt,
            UpdatedAt = createdVersion.UpdatedAt,
        };
    }


    public async Task<ModVersionDto> Update(UpdateModVersionDto mod_version)
    {
        ModVersion? updatedVersion = await context.ModVersions.FindAsync(mod_version.Id);
        
        
        if (updatedVersion == null)
        {
            throw new KeyNotFoundException($"Version with id {mod_version.Id} not found");
        }
        
        
        updatedVersion.Title = mod_version.Title;
        updatedVersion.UpdatedAt = DateTime.UtcNow;
        
        
        if (string.IsNullOrWhiteSpace(mod_version.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (mod_version.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.ModVersions.Update(updatedVersion);
        await context.SaveChangesAsync();


        return new ModVersionDto()
        {
            Id = updatedVersion.Id,
            Title = updatedVersion.Title,
            CreatedAt = updatedVersion.CreatedAt,
            UpdatedAt = updatedVersion.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        ModVersion? version = await context.ModVersions.FindAsync(id);
        
        if (version == null)
        {
            throw new KeyNotFoundException($"Version with id {id} not found");
        }
        
        context.ModVersions.Remove(version);
        await context.SaveChangesAsync();
    }
}