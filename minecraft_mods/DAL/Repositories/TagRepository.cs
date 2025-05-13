using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
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


    public async Task<PaginatedResult<TagDto>> GetByPage(int pageNumber, int pageSize)
    {
        var query = context.Tags.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        
        var items = tags.Select(t => new TagDto()
        {
            Id = t.Id,
            Title = t.Title,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();


        return new PaginatedResult<TagDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }


    public async Task<TagDto> GetById(Guid id)
    {
        Tag? tag = await context.Tags.FindAsync(id);


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
        
        
        updatedTag.Title = tag.Title;
        updatedTag.UpdatedAt = DateTime.UtcNow;
        
        
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
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }
}