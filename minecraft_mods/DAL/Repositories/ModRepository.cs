using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Mod;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class ModRepository(ApplicationContext context) : IRepository<ModDto, CreateModDto, UpdateModDto>
{
    public async Task<List<ModDto>> GetAll()
    {
        List<Mod> mods = await context.Mods.ToListAsync();
        List<ModDto> modsList = new List<ModDto>();
        
        
        foreach (var mod in mods)
        {
            ModDto modDto = new()
            {
                Id = mod.Id,
                Title = mod.Title,
                Description = mod.Description,
                IsClientside = mod.IsClientside,
                Downloads = mod.Downloads,
                Size = mod.Size
            };
            modsList.Add(modDto);
        }
        
        
        return modsList;
    }


    public async Task<ModDto> GetById(Guid id)
    {
        Mod? mod = await context.Mods.FindAsync(id);


        return new ModDto()
        {
            Id = mod.Id,
            Title = mod.Title,
            Description = mod.Description,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size
        };
    }


    public async Task<ModDto> Create(CreateModDto mod)
    {
        Mod createdMod = new()
        {
            Title = mod.Title,
            Description = mod.Description,
            IsClientside = mod.IsClientside,
            Downloads = mod.Downloads,
            Size = mod.Size
        };
        
        
        context.Mods.Add(createdMod);
        await context.SaveChangesAsync();


        return new ModDto()
        {
            Id = createdMod.Id,
            Title = createdMod.Title,
            Description = createdMod.Description,
            IsClientside = createdMod.IsClientside,
            Downloads = createdMod.Downloads,
            Size = createdMod.Size
        };
    }


    public async Task<ModDto> Update(UpdateModDto mod)
    {
        Mod? updatedMod = await context.Mods.FindAsync(mod.Id);
        
        
        updatedMod.Title = mod.Title;
        updatedMod.Description = mod.Description;
        updatedMod.IsClientside = mod.IsClientside;
        updatedMod.Downloads = mod.Downloads;
        updatedMod.Size = mod.Size;
        
        
        context.Mods.Update(updatedMod);
        await context.SaveChangesAsync();


        return new ModDto()
        {
            Id = updatedMod.Id,
            Title = updatedMod.Title,
            Description = updatedMod.Description,
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