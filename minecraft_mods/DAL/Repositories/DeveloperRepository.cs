using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Developer;
using DTO.Shared;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class DeveloperRepository(ApplicationContext context) : IRepository<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto>
{
    public async Task<List<DeveloperDto>> GetAll()
    {
        List<Developer> developers = await context.Developers.ToListAsync();
        
        
        return developers.Select(developer => new DeveloperDto()
        {
            Id = developer.Id,
            Nickname = developer.Nickname,
            CreatedAt = developer.CreatedAt,
            UpdatedAt = developer.UpdatedAt
        }).ToList();
    }
    
    
    public async Task<QueryParamsDto<DeveloperDto>> GetByPage(QueryParamsDto<DeveloperDto> queryParams)
    {
        var query = context.Developers.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = tags.Select(d => new DeveloperDto()
        {
            Id = d.Id,
            Nickname = d.Nickname,
            CreatedAt = d.CreatedAt,
            UpdatedAt = d.UpdatedAt
        }).ToList();


        return new QueryParamsDto<DeveloperDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<DeveloperDto> GetById(Guid id)
    {
        Developer? developer = await context.Developers.FindAsync(id);


        if (developer == null)
        {
            throw new KeyNotFoundException($"Developer with id {id} not found");
        }
        
        
        return new DeveloperDto()
        {
            Id = developer.Id,
            Nickname = developer.Nickname,
            CreatedAt = developer.CreatedAt,
            UpdatedAt = developer.UpdatedAt,
        };
    }


    public async Task<DeveloperDto> Create(CreateDeveloperDto developer)
    {
        Developer createdDeveloper = new()
        {
            Nickname = developer.Nickname,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        if (string.IsNullOrWhiteSpace(developer.Nickname))
        {
            throw new ArgumentException("Nickname cannot be empty");
        }
        
        if (developer.Nickname.Length > 100)
        {
            throw new ArgumentException("Nickname is too long (max 100 chars)");
        }
        
        
        context.Developers.Add(createdDeveloper);
        await context.SaveChangesAsync();


        return new DeveloperDto()
        {
            Id = createdDeveloper.Id,
            Nickname = createdDeveloper.Nickname,
            CreatedAt = createdDeveloper.CreatedAt,
            UpdatedAt = createdDeveloper.UpdatedAt,
        };
    }


    public async Task<DeveloperDto> Update(UpdateDeveloperDto developer)
    {
        Developer? updatedDeveloper = await context.Developers.FindAsync(developer.Id);
        
        
        if (updatedDeveloper == null)
        {
            throw new KeyNotFoundException($"Developer with id {developer.Id} not found");
        }
        
        
        updatedDeveloper.Nickname = developer.Nickname;
        updatedDeveloper.UpdatedAt = DateTime.UtcNow;
        
        
        if (string.IsNullOrWhiteSpace(developer.Nickname))
        {
            throw new ArgumentException("Nickname cannot be empty");
        }
        
        if (developer.Nickname.Length > 100)
        {
            throw new ArgumentException("Nickname is too long (max 100 chars)");
        }
        
        
        context.Developers.Update(updatedDeveloper);
        await context.SaveChangesAsync();


        return new DeveloperDto()
        {
            Id = updatedDeveloper.Id,
            Nickname = updatedDeveloper.Nickname,
            CreatedAt = updatedDeveloper.CreatedAt,
            UpdatedAt = updatedDeveloper.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        Developer? developer = await context.Developers.FindAsync(id);
        
        if (developer == null)
        {
            throw new KeyNotFoundException($"Developer with id {id} not found");
        }
        
        context.Developers.Remove(developer);
        await context.SaveChangesAsync();
    }
}