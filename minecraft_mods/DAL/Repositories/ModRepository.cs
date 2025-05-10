using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Mod;
using DTO.ModLoader;
using DTO.ModVersion;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class ModRepository(ApplicationContext context) : IRepository<ModDto, CreateModDto, UpdateModDto>
{
    public async Task<List<ModDto>> GetAll()
    {
        List<Mod> mods = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .ToListAsync();


        return mods.Select(mod => new ModDto()
        {
            Id = mod.Id,
            Title = mod.Title,
            Description = mod.Description,
            Versions = mod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
            }).ToList(),
            ModLoaders = mod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
            }).ToList(),
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size
        }).ToList();
    }


    public async Task<ModDto> GetById(Guid id)
    {
        Mod? mod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .FirstOrDefaultAsync(m => m.Id == id);


        return new ModDto()
        {
            Id = mod.Id,
            Title = mod.Title,
            Description = mod.Description,
            Versions = mod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
            }).ToList(),
            ModLoaders = mod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
            }).ToList(),
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size
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
        
        
        Mod createdMod = new()
        {
            Title = mod.Title,
            Description = mod.Description,
            Versions = versions,
            ModLoaders = loaders,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size
        };
        
        
        context.Mods.Add(createdMod);
        await context.SaveChangesAsync();
        
        
        createdMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .FirstOrDefaultAsync(m => m.Id == createdMod.Id);


        return new ModDto()
        {
            Id = createdMod.Id,
            Title = createdMod.Title,
            Description = createdMod.Description,
            Versions = createdMod.Versions.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
            }).ToList(),
            ModLoaders = createdMod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
            }).ToList(),
            IsClientside = createdMod.IsClientside,
            Downloads = createdMod.Downloads,
            Size = createdMod.Size
        };
    }


    public async Task<ModDto> Update(UpdateModDto mod)
    {
        Mod? updatedMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .FirstOrDefaultAsync(m => m.Id == mod.Id);
        
        
        var versions = await context.ModVersions
            .Where(v => mod.VersionIds.Contains(v.Id))
            .ToListAsync();
        
        
        var loaders = await context.ModLoaders
            .Where(l => mod.ModLoaderIds.Contains(l.Id))
            .ToListAsync();
        
        
        updatedMod.Title = mod.Title;
        updatedMod.Description = mod.Description;
        updatedMod.Versions = versions;
        updatedMod.ModLoaders = loaders;
        updatedMod.IsClientside = mod.IsClientside;
        updatedMod.Downloads = mod.Downloads;
        updatedMod.Size = mod.Size;
        
        
        context.Mods.Update(updatedMod);
        await context.SaveChangesAsync();
        
        
        updatedMod = await context.Mods
            .Include(m => m.Versions)
            .Include(m => m.ModLoaders)
            .FirstOrDefaultAsync(m => m.Id == mod.Id);


        return new ModDto()
        {
            Id = updatedMod.Id,
            Title = updatedMod.Title,
            Description = updatedMod.Description,
            Versions = updatedMod.Versions?.Select(v => new ModVersionDto()
            {
                Id = v.Id,
                Title = v.Title,
            }).ToList() ?? new List<ModVersionDto>(),
            ModLoaders = updatedMod.ModLoaders.Select(l => new ModLoaderDto()
            {
                Id = l.Id,
                Title = l.Title,
            }).ToList() ?? new List<ModLoaderDto>(),
            IsClientside = updatedMod.IsClientside,
            Downloads = updatedMod.Downloads,
            Size = updatedMod.Size
        };
    }


    public async Task Delete(Guid id)
    {
        Mod? mod = await context.Mods.FindAsync(id);
        context.Mods.Remove(mod);
        await context.SaveChangesAsync();
    }
}