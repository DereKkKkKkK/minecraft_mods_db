﻿using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Difficulty;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class DifficultyRepository(ApplicationContext context) : IRepository<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto>
{
    public async Task<List<DifficultyDto>> GetAll()
    {
        List<Difficulty> difficulties = await context.Difficulties.ToListAsync();
        List<DifficultyDto> difficultiesList = new List<DifficultyDto>();
        
        
        foreach (var difficulty in difficulties)
        {
            DifficultyDto DifficultyDto = new()
            {
                Id = difficulty.Id,
                Title = difficulty.Title
            };
            difficultiesList.Add(DifficultyDto);
        }
        
        
        return difficultiesList;
    }


    public async Task<DifficultyDto> GetById(Guid id)
    {
        Difficulty? difficulty = await context.Difficulties.FindAsync(id);


        return new DifficultyDto()
        {
            Id = difficulty.Id,
            Title = difficulty.Title
        };
    }


    public async Task<DifficultyDto> Create(CreateDifficultyDto difficulty)
    {
        Difficulty createdDifficulty = new()
        {
            Title = difficulty.Title,
        };
        
        
        context.Difficulties.Add(createdDifficulty);
        await context.SaveChangesAsync();


        return new DifficultyDto()
        {
            Id = createdDifficulty.Id,
            Title = createdDifficulty.Title
        };
    }


    public async Task<DifficultyDto> Update(UpdateDifficultyDto difficulty)
    {
        Difficulty? updatedDifficulty = await context.Difficulties.FindAsync(difficulty.Id);
        
        
        updatedDifficulty.Title = difficulty.Title;
        
        
        context.Difficulties.Update(updatedDifficulty);
        await context.SaveChangesAsync();


        return new DifficultyDto()
        {
            Id = updatedDifficulty.Id,
            Title = updatedDifficulty.Title,
        };
    }


    public async Task Delete(Guid id)
    {
        Difficulty? difficulty = await context.Difficulties.FindAsync(id);
        context.Difficulties.Remove(difficulty);
        await context.SaveChangesAsync();
    }
}