using DTO.Shared;

namespace DAL.Repositories;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Focus;
using Microsoft.EntityFrameworkCore;

public class FocusRepository(ApplicationContext context) : IRepository<FocusDto, CreateFocusDto, UpdateFocusDto>
{
    public async Task<List<FocusDto>> GetAll()
    {
        List<Focus> focuses = await context.Focuses.ToListAsync();
        
        
        return focuses.Select(difficulty => new FocusDto()
        {
            Id = difficulty.Id,
            Name = difficulty.Name,
            CreatedAt = difficulty.CreatedAt,
            UpdatedAt = difficulty.UpdatedAt
        }).ToList();
    }
    
    
    public async Task<QueryParamsDto<FocusDto>> GetByPage(QueryParamsDto<FocusDto> queryParams)
    {
        var query = context.Focuses.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = tags.Select(f => new FocusDto()
        {
            Id = f.Id,
            Name = f.Name,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt,
        }).ToList();


        return new QueryParamsDto<FocusDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<FocusDto> GetById(Guid id)
    {
        Focus? focus = await context.Focuses.FindAsync(id);

        
        if (focus == null)
        {
            throw new KeyNotFoundException($"Focus with id {id} not found");
        }
        

        return new FocusDto()
        {
            Id = focus.Id,
            Name = focus.Name,
            CreatedAt = focus.CreatedAt,
            UpdatedAt = focus.UpdatedAt,
        };
    }


    public async Task<FocusDto> Create(CreateFocusDto focus)
    {
        Focus createdFocus = new()
        {
            Name = focus.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        if (string.IsNullOrWhiteSpace(focus.Name))
        {
            throw new ArgumentException("Name cannot be empty");
        }
        
        if (focus.Name.Length > 100)
        {
            throw new ArgumentException("Name is too long (max 100 chars)");
        }
        
        
        context.Focuses.Add(createdFocus);
        await context.SaveChangesAsync();


        return new FocusDto()
        {
            Id = createdFocus.Id,
            Name = createdFocus.Name,
            CreatedAt = createdFocus.CreatedAt,
            UpdatedAt = createdFocus.UpdatedAt,
        };
    }


    public async Task<FocusDto> Update(UpdateFocusDto focus)
    {
        Focus? updatedFocus = await context.Focuses.FindAsync(focus.Id);
        
        
        if (updatedFocus == null)
        {
            throw new KeyNotFoundException($"Focus with id {focus.Id} not found");
        }
        
        
        updatedFocus.Name = focus.Name;
        updatedFocus.UpdatedAt = DateTime.UtcNow;
        
        
        if (string.IsNullOrWhiteSpace(focus.Name))
        {
            throw new ArgumentException("Name cannot be empty");
        }
        
        if (focus.Name.Length > 100)
        {
            throw new ArgumentException("Name is too long (max 100 chars)");
        }
        
        
        context.Focuses.Update(updatedFocus);
        await context.SaveChangesAsync();


        return new FocusDto()
        {
            Id = updatedFocus.Id,
            Name = updatedFocus.Name,
            CreatedAt = updatedFocus.CreatedAt,
            UpdatedAt = updatedFocus.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        Focus? focus = await context.Focuses.FindAsync(id);
        
        if (focus == null)
        {
            throw new KeyNotFoundException($"Focus with id {id} not found");
        }
        
        context.Focuses.Remove(focus);
        await context.SaveChangesAsync();
    }
}