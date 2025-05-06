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
        List<TagDto> tagsList = new List<TagDto>();
        
        
        foreach (var tag in tags)
        {
            TagDto TagDto = new()
            {
                Id = tag.Id,
                Title = tag.Title
            };
            tagsList.Add(TagDto);
        }
        
        
        return tagsList;
    }


    public async Task<TagDto> GetById(Guid id)
    {
        Tag? tag = await context.Tags.FindAsync(id);


        return new TagDto()
        {
            Id = tag.Id,
            Title = tag.Title
        };
    }


    public async Task<TagDto> Create(CreateTagDto tag)
    {
        Tag createdTag = new()
        {
            Title = tag.Title,
        };
        
        
        context.Tags.Add(createdTag);
        await context.SaveChangesAsync();


        return new TagDto()
        {
            Id = createdTag.Id,
            Title = createdTag.Title
        };
    }


    public async Task<TagDto> Update(UpdateTagDto tag)
    {
        Tag? updatedTag = await context.Tags.FindAsync(tag.Id);
        
        
        updatedTag.Title = tag.Title;
        
        
        context.Tags.Update(updatedTag);
        await context.SaveChangesAsync();


        return new TagDto()
        {
            Id = updatedTag.Id,
            Title = updatedTag.Title,
        };
    }


    public async Task Delete(Guid id)
    {
        Tag? tag = await context.Tags.FindAsync(id);
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }
}