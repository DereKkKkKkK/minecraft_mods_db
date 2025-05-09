﻿namespace DAL.Repositories;
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
        List<FocusDto> focusList = new List<FocusDto>();
        
        
        
        foreach (var focus in focuses)
        {
            FocusDto FocusDto = new()
            {
                Id = focus.Id,
                Name = focus.Name
            };
            focusList.Add(FocusDto);
        }
        
        
        return focusList;
    }


    public async Task<FocusDto> GetById(Guid id)
    {
        Focus? focus = await context.Focuses.FindAsync(id);


        return new FocusDto()
        {
            Id = focus.Id,
            Name = focus.Name
        };
    }


    public async Task<FocusDto> Create(CreateFocusDto focus)
    {
        Focus createdFocus = new()
        {
            Name = focus.Name,
        };
        
        
        context.Focuses.Add(createdFocus);
        await context.SaveChangesAsync();


        return new FocusDto()
        {
            Id = createdFocus.Id,
            Name = createdFocus.Name
        };
    }


    public async Task<FocusDto> Update(UpdateFocusDto focus)
    {
        Focus? updatedFocus = await context.Focuses.FindAsync(focus.Id);
        
        
        updatedFocus.Name = focus.Name;
        
        
        context.Focuses.Update(updatedFocus);
        await context.SaveChangesAsync();


        return new FocusDto()
        {
            Id = updatedFocus.Id,
            Name = updatedFocus.Name,
        };
    }


    public async Task Delete(Guid id)
    {
        Focus? focus = await context.Focuses.FindAsync(id);
        context.Focuses.Remove(focus);
        await context.SaveChangesAsync();
    }
}