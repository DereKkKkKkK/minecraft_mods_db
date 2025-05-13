using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.ModVersion;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VersionRepository(ApplicationContext context) : IRepository<ModVersionDto, CreateModVersionDto, UpdateModVersionDto>
{
    public async Task<List<ModVersionDto>> GetAll()
    {
        List<ModVersion> versions = await context.ModVersions.ToListAsync();
        List<ModVersionDto> versionsList = new List<ModVersionDto>();
        
        
        foreach (var mod_version in versions)
        {
            ModVersionDto modVersionDto = new()
            {
                Id = mod_version.Id,
                Title = mod_version.Title,
                CreatedAt = mod_version.CreatedAt,
                UpdatedAt = mod_version.UpdatedAt,
            };
            versionsList.Add(modVersionDto);
        }
        
        
        return versionsList;
    }


    public async Task<ModVersionDto> GetById(Guid id)
    {
        ModVersion? mod_version = await context.ModVersions.FindAsync(id);


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
        
        
        updatedVersion.Title = mod_version.Title;
        
        
        context.ModVersions.Update(updatedVersion);
        await context.SaveChangesAsync();


        return new ModVersionDto()
        {
            Id = updatedVersion.Id,
            Title = updatedVersion.Title,
            CreatedAt = updatedVersion.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
    }


    public async Task Delete(Guid id)
    {
        ModVersion? version = await context.ModVersions.FindAsync(id);
        context.ModVersions.Remove(version);
        await context.SaveChangesAsync();
    }
}