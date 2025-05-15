using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Mod;
using DTO.ModLoader;
using DTO.ModVersion;
using DTO.Shared;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class ModRepository(ApplicationContext context) : IRepository<ModDto, CreateModDto, UpdateModDto>
{
    public async Task<List<ModDto>> GetAll()
    {
        List<Mod> mods = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .ToListAsync();


        return mods.Select(mod => new ModDto()
        {
            Id = mod.Id,
            Title = mod.Title,
            Description = mod.Description,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size,
            Versions = mod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList(),
            ModLoaders = mod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList(),
            Tags = mod.Tags.Select(t => new TagDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),
            CreatedAt = mod.CreatedAt,
            UpdatedAt = mod.UpdatedAt
        }).ToList();
    }


    public async Task<QueryParamsDto<ModDto>> GetByPage(int pageNumber, int pageSize)
    {
        var query = context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .AsNoTracking();
        
        
        var totalCount = await context.Mods.CountAsync();
        
        
        var mods = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        
        var items = mods.Select(m => new ModDto()
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            IsClientside = m.IsClientside,
            Downloads = m.Downloads,
            Size = m.Size,
            Versions = m.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList(),
            ModLoaders = m.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList(),
            Tags = m.Tags.Select(t => new TagDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),
            CreatedAt = m.CreatedAt,
            UpdatedAt = m.UpdatedAt
        }).ToList();


        return new QueryParamsDto<ModDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }


    public async Task<ModDto> GetById(Guid id)
    {
        Mod? mod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .FirstOrDefaultAsync(m => m.Id == id);


        return new ModDto()
        {
            Id = mod.Id,
            Title = mod.Title,
            Description = mod.Description,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size,
            Versions = mod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList(),
            ModLoaders = mod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList(),
            Tags = mod.Tags.Select(t => new TagDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),
            CreatedAt = mod.CreatedAt,
            UpdatedAt = mod.UpdatedAt
        };
    }


    public async Task<ModDto> Create(CreateModDto mod)
    {
        var versions = await context.ModVersions
            .Where(v => mod.VersionIds.Contains(v.Id))
            .ToListAsync();
        
        
        var loaders = await context.ModLoaders
            .Where(l => mod.ModLoaderIds.Contains(l.Id))
            .ToListAsync();
        
        
        var tags = await context.Tags
            .Where(t => mod.TagIds.Contains(t.Id))
            .ToListAsync();
        
        
        Mod createdMod = new()
        {
            Title = mod.Title,
            Description = mod.Description,
            Versions = versions,
            ModLoaders = loaders,
            Tags = tags,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        context.Mods.Add(createdMod);
        await context.SaveChangesAsync();
        
        
        createdMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .FirstOrDefaultAsync(m => m.Id == createdMod.Id);


        return new ModDto()
        {
            Id = createdMod.Id,
            Title = createdMod.Title,
            Description = createdMod.Description,
            IsClientside = createdMod.IsClientside,
            Downloads = createdMod.Downloads,
            Size = createdMod.Size,
            Versions = createdMod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList(),
            ModLoaders = createdMod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList(),
            Tags = createdMod.Tags.Select(t => new TagDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),
            CreatedAt = createdMod.CreatedAt,
            UpdatedAt = createdMod.UpdatedAt
        };
    }


    public async Task<ModDto> Update(UpdateModDto mod)
    {
        Mod? updatedMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .FirstOrDefaultAsync(m => m.Id == mod.Id);
        
        
        var versions = await context.ModVersions
            .Where(v => mod.VersionIds.Contains(v.Id))
            .ToListAsync();
        
        
        var loaders = await context.ModLoaders
            .Where(l => mod.ModLoaderIds.Contains(l.Id))
            .ToListAsync();
        
        
        var tags = await context.Tags
            .Where(t => mod.TagIds.Contains(t.Id))
            .ToListAsync();
        
        
        updatedMod.Title = mod.Title;
        updatedMod.Description = mod.Description;
        updatedMod.Versions = versions;
        updatedMod.ModLoaders = loaders;
        updatedMod.Tags = tags;
        updatedMod.IsClientside = mod.IsClientside;
        updatedMod.Downloads = mod.Downloads;
        updatedMod.Size = mod.Size;
        updatedMod.UpdatedAt = DateTime.UtcNow;
        
        
        context.Mods.Update(updatedMod);
        await context.SaveChangesAsync();
        
        
        updatedMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .Include(m => m.Tags)
            .FirstOrDefaultAsync(m => m.Id == mod.Id);


        return new ModDto()
        {
            Id = updatedMod.Id,
            Title = updatedMod.Title,
            Description = updatedMod.Description,
            IsClientside = updatedMod.IsClientside,
            Downloads = updatedMod.Downloads,
            Size = updatedMod.Size,
            Versions = updatedMod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
            }).ToList(),
            ModLoaders = updatedMod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList(),
            Tags = updatedMod.Tags.Select(t => new TagDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList(),
            CreatedAt = updatedMod.CreatedAt,
            UpdatedAt = updatedMod.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        Mod? mod = await context.Mods.FindAsync(id);
        context.Mods.Remove(mod);
        await context.SaveChangesAsync();
    }
}