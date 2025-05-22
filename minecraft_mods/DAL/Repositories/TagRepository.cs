using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Shared;
using DTO.Tag;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class TagRepository(ApplicationContext context) : IRepository<TagDto, CreateTagDto, UpdateTagDto>
{
    public async Task<List<TagDto>> GetAll()
    {
        List<Tag> tags = await context.Tags.ToListAsync();
        
        
        return tags.Select(t => new TagDto()
        {
            Id = t.Id,
            Title = t.Title,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
        }).ToList();
    }


    public async Task<QueryParamsDto<TagDto>> GetByPage(QueryParamsDto<TagDto> queryParams)
    {
        var query = context.Tags.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        
        var items = tags.Select(t => new TagDto()
        {
            Id = t.Id,
            Title = t.Title,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();


        return new QueryParamsDto<TagDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
    }


    public async Task<TagDto> GetById(Guid id)
    {
        Tag? tag = await context.Tags.FindAsync(id);
        
        
        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with id {id} not found");
        }


        return new TagDto()
        {
            Id = tag.Id,
            Title = tag.Title,
            CreatedAt = tag.CreatedAt,
            UpdatedAt = tag.UpdatedAt,
        };
    }


    public async Task<TagDto> Create(CreateTagDto tag)
    {
        Tag createdTag = new()
        {
            Title = tag.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        if (string.IsNullOrWhiteSpace(tag.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (tag.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.Tags.Add(createdTag);
        await context.SaveChangesAsync();


        return new TagDto()
        {
            Id = createdTag.Id,
            Title = createdTag.Title,
            CreatedAt = createdTag.CreatedAt,
            UpdatedAt = createdTag.UpdatedAt,
        };
    }


    public async Task<TagDto> Update(UpdateTagDto tag)
    {
        Tag? updatedTag = await context.Tags.FindAsync(tag.Id);
        
        
        if (updatedTag == null)
        {
            throw new KeyNotFoundException($"Tag with id {tag.Id} not found");
        }
        
        
        updatedTag.Title = tag.Title;
        updatedTag.UpdatedAt = DateTime.UtcNow;
        
        
        if (string.IsNullOrWhiteSpace(tag.Title))
        {
            throw new ArgumentException("Title cannot be empty");
        }
        
        if (tag.Title.Length > 100)
        {
            throw new ArgumentException("Title is too long (max 100 chars)");
        }
        
        
        context.Tags.Update(updatedTag);
        await context.SaveChangesAsync();


        return new TagDto()
        {
            Id = updatedTag.Id,
            Title = updatedTag.Title,
            CreatedAt = updatedTag.CreatedAt,
            UpdatedAt = updatedTag.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        Tag? tag = await context.Tags.FindAsync(id);
        
        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with id {id} not found");
        }
        
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }
}