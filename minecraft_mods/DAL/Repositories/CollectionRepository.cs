using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Collection;
using DTO.Focus;
using DTO.Mod;
using DTO.ModLoader;
using DTO.ModVersion;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class CollectionRepository(ApplicationContext context) : IRepository<CollectionDto, CreateCollectionDto, UpdateCollectionDto>
{
    public async Task<List<CollectionDto>> GetAll()
    {
        List<Collection> collections = await context.Collections
            .Include(c => c.Mods)
                .ThenInclude(m => m.Versions)
            .Include(c => c.Mods)
                .ThenInclude(m => m.ModLoaders)
            .Include(c => c.Mods)
                .ThenInclude(m => m.Tags)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .ToListAsync();
        return collections.Select(collection => new CollectionDto()
        {
            Id = collection.Id,
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
           
            Mods = collection.Mods.Select(m => new ModDto()
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
                }).ToList(),
                ModLoaders = m.ModLoaders.Select(l => new ModLoaderDto()
                {
                    Id = l.Id,
                    Title = l.Title
                }).ToList(),
                Tags = m.Tags.Select(t => new TagDto()
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToList()
            }).ToList(),
            Focuses = collection.Focuses.Select(f => new FocusDto()
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList(),
            Version = new ModVersionDto()
            {
                Id = collection.Version.Id,
                Title = collection.Version.Title
            },
            ModLoader = new ModLoaderDto()
            {
                Id = collection.ModLoader.Id,
                Title = collection.ModLoader.Title
            }
        }).ToList();
    }


    public async Task<CollectionDto> GetById(Guid id)
    {
        Collection? collection = await context.Collections
            .Include(c => c.Mods)
                .ThenInclude(m => m.Versions)
            .Include(c => c.Mods)
                .ThenInclude(m => m.ModLoaders)
            .Include(c => c.Mods)
                .ThenInclude(m => m.Tags)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return new CollectionDto()
        {
            Id = collection.Id,
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
           
            Mods = collection.Mods.Select(m => new ModDto()
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
                }).ToList(),
                ModLoaders = m.ModLoaders.Select(l => new ModLoaderDto()
                {
                    Id = l.Id,
                    Title = l.Title,
                }).ToList(),
                Tags = m.Tags.Select(t => new TagDto()
                {
                    Id = t.Id,
                    Title = t.Title,
                }).ToList(),
            }).ToList(),
            Focuses = collection.Focuses.Select(f => new FocusDto()
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList(),
            Version = new ModVersionDto()
            {
                Id = collection.Version.Id,
                Title = collection.Version.Title
            },
            ModLoader = new ModLoaderDto()
            {
                Id = collection.Version.Id,
                Title = collection.Version.Title
            }
        };
    }


    public async Task<CollectionDto> Create(CreateCollectionDto collection)
    {
        var mods = await context.Mods
            .Where(m => collection.ModsIds.Contains(m.Id))
            .ToListAsync();
        
        
        var focuses = await context.Focuses
            .Where(f => collection.FocusesIds.Contains(f.Id))
            .ToListAsync();
        
        var version = await context.ModVersions.FindAsync(collection.VersionId);
    
        var modLoader = await context.ModLoaders.FindAsync(collection.ModLoaderId);
      
            
        
        Collection createdCollection = new()
        {
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
            Version = version,
            ModLoader = modLoader,
            Mods = mods,
            Focuses = focuses
        };


        context.Collections.Add(createdCollection);
        await context.SaveChangesAsync();


        return new CollectionDto()
        {
            Id = createdCollection.Id,
            Name = createdCollection.Name,
            TimeToComplete = createdCollection.TimeToComplete,
            Mods = createdCollection.Mods.Select(m => new ModDto()
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
                }).ToList(),
                ModLoaders = m.ModLoaders.Select(l => new ModLoaderDto()
                {
                    Id = l.Id,
                    Title = l.Title,
                }).ToList(),
                Tags = m.Tags.Select(t => new TagDto()
                {
                    Id = t.Id,
                    Title = t.Title,
                }).ToList(),
            }).ToList(),
            Focuses = createdCollection.Focuses.Select(f => new FocusDto()
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList(),
            Version = new ModVersionDto()
            {
                Id = createdCollection.Version.Id,
                Title = createdCollection.Version.Title
            },
            ModLoader = new ModLoaderDto()
            {
                Id = createdCollection.Version.Id,
                Title = createdCollection.Version.Title
            }
        };
    }


    public async Task<CollectionDto> Update(UpdateCollectionDto collection)
    {
        Collection? updatedCollection = await context.Collections
            .Include(c => c.Mods)
                .ThenInclude(m => m.Versions)
            .Include(c => c.Mods)
                .ThenInclude(m => m.ModLoaders)
            .Include(c => c.Mods)
                .ThenInclude(m => m.Tags)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .FirstOrDefaultAsync(c => c.Id == collection.Id);

        var mods = await context.Mods
            .Where(m => collection.ModsIds.Contains(m.Id))
            .ToListAsync();
        
        
        var focuses = await context.Focuses
            .Where(f => collection.FocusesIds.Contains(f.Id))
            .ToListAsync();
        
        var version = await context.ModVersions.FindAsync(collection.VersionId);
    
        var modLoader = await context.ModLoaders.FindAsync(collection.ModLoaderId);
        
        updatedCollection.Name = collection.Name;
        updatedCollection.TimeToComplete = collection.TimeToComplete;
        updatedCollection.Mods = mods;
        updatedCollection.Focuses = focuses;
        updatedCollection.Version = version;
        updatedCollection.ModLoader = modLoader;
        
        
        context.Collections.Update(updatedCollection);
        await context.SaveChangesAsync();


        return new CollectionDto()
        {
            Id = updatedCollection.Id,
            Name = updatedCollection.Name,
            TimeToComplete = updatedCollection.TimeToComplete,
            Mods = updatedCollection.Mods.Select(m => new ModDto()
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
                }).ToList(),
                ModLoaders = m.ModLoaders.Select(l => new ModLoaderDto()
                {
                    Id = l.Id,
                    Title = l.Title,
                }).ToList(),
                Tags = m.Tags.Select(t => new TagDto()
                {
                    Id = t.Id,
                    Title = t.Title,
                }).ToList(),
            }).ToList(),
            Focuses = updatedCollection.Focuses.Select(f => new FocusDto()
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList(),
            Version = new ModVersionDto()
            {
                Id = updatedCollection.Version.Id,
                Title = updatedCollection.Version.Title
            },
            ModLoader = new ModLoaderDto()
            {
                Id = updatedCollection.Version.Id,
                Title = updatedCollection.Version.Title
            }
        };
    }


    public async Task Delete(Guid id)
    {
        Collection? collection = await context.Collections.FindAsync(id);
        context.Collections.Remove(collection);
        await context.SaveChangesAsync();
    }
}
