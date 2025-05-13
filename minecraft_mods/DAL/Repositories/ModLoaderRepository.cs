using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.ModLoader;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ModLoaderRepository(ApplicationContext context) : IRepository<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto>
{
    public async Task<List<ModLoaderDto>> GetAll()
    {
        List<ModLoader> modLoaders = await context.ModLoaders.ToListAsync();
        List<ModLoaderDto> modLoadersList = new List<ModLoaderDto>();
        
        
        foreach (var modLoader in modLoaders)
        {
            ModLoaderDto ModLoaderDto = new()
            {
                Id = modLoader.Id,
                Title = modLoader.Title,
                CreatedAt = modLoader.CreatedAt,
                UpdatedAt = modLoader.UpdatedAt,
            };
            modLoadersList.Add(ModLoaderDto);
        }
        
        
        return modLoadersList;
    }


    public async Task<ModLoaderDto> GetById(Guid id)
    {
        ModLoader? modLoader = await context.ModLoaders.FindAsync(id);


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
        
        
        updatedModLoader.Title = modLoader.Title;
        
        
        context.ModLoaders.Update(updatedModLoader);
        await context.SaveChangesAsync();


        return new ModLoaderDto()
        {
            Id = updatedModLoader.Id,
            Title = updatedModLoader.Title,
            CreatedAt = updatedModLoader.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
    }


    public async Task Delete(Guid id)
    {
        ModLoader? modLoader = await context.ModLoaders.FindAsync(id);
        context.ModLoaders.Remove(modLoader);
        await context.SaveChangesAsync();
    }
}