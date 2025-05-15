using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Difficulty;
using DTO.Shared;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class DifficultyRepository(ApplicationContext context) : IRepository<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto>
{
    public async Task<List<DifficultyDto>> GetAll()
    {
        List<Difficulty> difficulties = await context.Difficulties.ToListAsync();
        
        
        return difficulties.Select(difficulty => new DifficultyDto()
        {
            Id = difficulty.Id,
            Title = difficulty.Title,
            CreatedAt = difficulty.CreatedAt,
            UpdatedAt = difficulty.UpdatedAt
        }).ToList();
    }
    
    
    public async Task<QueryParamsDto<DifficultyDto>> GetByPage(int pageNumber, int pageSize)
    {
        var query = context.Difficulties.AsNoTracking();
        var totalCount = await query.CountAsync();
        var tags = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        
        var items = tags.Select(d => new DifficultyDto()
        {
            Id = d.Id,
            Title = d.Title,
            CreatedAt = d.CreatedAt,
            UpdatedAt = d.UpdatedAt
        }).ToList();


        return new QueryParamsDto<DifficultyDto>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }


    public async Task<DifficultyDto> GetById(Guid id)
    {
        Difficulty? difficulty = await context.Difficulties.FindAsync(id);


        return new DifficultyDto()
        {
            Id = difficulty.Id,
            Title = difficulty.Title,
            CreatedAt = difficulty.CreatedAt,
            UpdatedAt = difficulty.UpdatedAt,
        };
    }


    public async Task<DifficultyDto> Create(CreateDifficultyDto difficulty)
    {
        Difficulty createdDifficulty = new()
        {
            Title = difficulty.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        
        context.Difficulties.Add(createdDifficulty);
        await context.SaveChangesAsync();


        return new DifficultyDto()
        {
            Id = createdDifficulty.Id,
            Title = createdDifficulty.Title,
            CreatedAt = createdDifficulty.CreatedAt,
            UpdatedAt = createdDifficulty.UpdatedAt,
        };
    }


    public async Task<DifficultyDto> Update(UpdateDifficultyDto difficulty)
    {
        Difficulty? updatedDifficulty = await context.Difficulties.FindAsync(difficulty.Id);
        
        
        updatedDifficulty.Title = difficulty.Title;
        updatedDifficulty.UpdatedAt = DateTime.UtcNow;
        
        
        context.Difficulties.Update(updatedDifficulty);
        await context.SaveChangesAsync();


        return new DifficultyDto()
        {
            Id = updatedDifficulty.Id,
            Title = updatedDifficulty.Title,
            CreatedAt = updatedDifficulty.CreatedAt,
            UpdatedAt = updatedDifficulty.UpdatedAt
        };
    }


    public async Task Delete(Guid id)
    {
        Difficulty? difficulty = await context.Difficulties.FindAsync(id);
        context.Difficulties.Remove(difficulty);
        await context.SaveChangesAsync();
    }
}