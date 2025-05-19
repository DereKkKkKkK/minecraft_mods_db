using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Collection;
using DTO.Difficulty;
using DTO.Focus;
using DTO.Mod;
using DTO.ModLoader;
using DTO.ModVersion;
using DTO.Shared;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CollectionRepository(ApplicationContext context) : IRepository<CollectionDto, CreateCollectionDto, UpdateCollectionDto>
{
    public async Task<List<CollectionDto>> GetAll()
    {
        List<Collection> collections = await context.Collections
            .Include(c => c.Mods)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .Include(c => c.Difficulty)
            .ToListAsync();

        return collections.Select(collection => new CollectionDto()
        {
            Id = collection.Id,
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
            CreatedAt = collection.CreatedAt,
            UpdatedAt = collection.UpdatedAt,
            
            Mods = collection.Mods.Select(m => new ModForCollectionDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsClientside = m.IsClientside,
                Downloads = m.Downloads,
                Size = m.Size
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
            },
            
            Difficulty = new DifficultyDto()
            {
                Id = collection.Difficulty.Id,
                Title = collection.Difficulty.Title
            }
        }).ToList();
    }


    public async Task<QueryParamsDto<CollectionDto>> GetByPage(QueryParamsDto<CollectionDto> queryParams)
    {
        var query = context.Collections
            .Include(m => m.Mods)
            .Include(m => m.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .Include(c => c.Difficulty)
            .AsNoTracking();


        var totalCount = await context.Mods.CountAsync();


        var collections = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = collections.Select(c => new CollectionDto()
        {
            Id = c.Id,
            Name = c.Name,
            TimeToComplete = c.TimeToComplete,
            Mods = c.Mods.Select(m => new ModForCollectionDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsClientside = m.IsClientside,
                Downloads = m.Downloads,
                Size = m.Size,
            }).ToList(),
            Focuses = c.Focuses.Select(f => new FocusDto()
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList(),
            Version = new ModVersionDto()
            {
                Id = c.Version.Id,
                Title = c.Version.Title
            },
            ModLoader = new ModLoaderDto()
            {
                Id = c.ModLoader.Id,
                Title = c.ModLoader.Title
            },
            Difficulty = new DifficultyDto()
            {
                Id = c.Difficulty.Id,
                Title = c.Difficulty.Title
            },
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
        }).ToList();


        return new QueryParamsDto<CollectionDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<CollectionDto> GetById(Guid id)
    {
        Collection? collection = await context.Collections
            .Include(c => c.Mods)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .Include(c => c.Difficulty)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return new CollectionDto()
        {
            Id = collection.Id,
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
            Mods = collection.Mods.Select(m => new ModForCollectionDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsClientside = m.IsClientside,
                Downloads = m.Downloads,
                Size = m.Size,
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
            },
            Difficulty = new DifficultyDto()
            {
                Id = collection.Difficulty.Id,
                Title = collection.Difficulty.Title
            },
            CreatedAt = collection.CreatedAt,
            UpdatedAt = collection.UpdatedAt
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
        var difficulty = await context.Difficulties.FindAsync(collection.DifficultyId);
        
        Collection createdCollection = new()
        {
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete,
            Mods = mods,
            Focuses = focuses,
            Version = version,
            ModLoader = modLoader,
            Difficulty = difficulty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Collections.Add(createdCollection);
        await context.SaveChangesAsync();

        return new CollectionDto()
        {
            Id = createdCollection.Id,
            Name = createdCollection.Name,
            TimeToComplete = createdCollection.TimeToComplete,
            Mods = createdCollection.Mods.Select(m => new ModForCollectionDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsClientside = m.IsClientside,
                Downloads = m.Downloads,
                Size = m.Size,
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
                Id = createdCollection.ModLoader.Id,
                Title = createdCollection.ModLoader.Title
            },
            Difficulty = new DifficultyDto()
            {
                Id = createdCollection.Difficulty.Id,
                Title = createdCollection.Difficulty.Title
            },
            CreatedAt = createdCollection.CreatedAt,
            UpdatedAt = createdCollection.UpdatedAt
        };
    }

    
    public async Task<CollectionDto> Update(UpdateCollectionDto collection)
    {
        Collection? updatedCollection = await context.Collections
            .Include(c => c.Mods)
            .Include(c => c.Focuses)
            .Include(c => c.Version)
            .Include(c => c.ModLoader)
            .Include(c => c.Difficulty)
            .FirstOrDefaultAsync(c => c.Id == collection.Id);

        var mods = await context.Mods
            .Where(m => collection.ModsIds.Contains(m.Id))
            .ToListAsync();
        
        var focuses = await context.Focuses
            .Where(f => collection.FocusesIds.Contains(f.Id))
            .ToListAsync();
        
        var version = await context.ModVersions.FindAsync(collection.VersionId);
        var modLoader = await context.ModLoaders.FindAsync(collection.ModLoaderId);
        var difficulty = await context.Difficulties.FindAsync(collection.DifficultyId);
        
        updatedCollection.Name = collection.Name;
        updatedCollection.TimeToComplete = collection.TimeToComplete;
        updatedCollection.Mods = mods;
        updatedCollection.Focuses = focuses;
        updatedCollection.Version = version;
        updatedCollection.ModLoader = modLoader;
        updatedCollection.Difficulty = difficulty;
        updatedCollection.UpdatedAt = DateTime.UtcNow;
      
        
        context.Collections.Update(updatedCollection);
        await context.SaveChangesAsync();

        return new CollectionDto()
        {
            Id = updatedCollection.Id,
            Name = updatedCollection.Name,
            TimeToComplete = updatedCollection.TimeToComplete,
            Mods = updatedCollection.Mods.Select(m => new ModForCollectionDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsClientside = m.IsClientside,
                Downloads = m.Downloads,
                Size = m.Size,
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
                Id = updatedCollection.ModLoader.Id,
                Title = updatedCollection.ModLoader.Title
            },
            Difficulty = new DifficultyDto()
            {
                Id = updatedCollection.Difficulty.Id,
                Title = updatedCollection.Difficulty.Title
            },
            CreatedAt = updatedCollection.CreatedAt,
            UpdatedAt = updatedCollection.UpdatedAt
        };
    }

    
    public async Task Delete(Guid id)
    {
        Collection? collection = await context.Collections.FindAsync(id);
        context.Collections.Remove(collection);
        await context.SaveChangesAsync();
    }
}