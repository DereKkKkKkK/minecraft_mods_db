using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Developer;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class DeveloperRepository(ApplicationContext context) : IRepository<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto>
{
    public async Task<List<DeveloperDto>> GetAll()
    {
        List<Developer> developers = await context.Developers.ToListAsync();
        List<DeveloperDto> developersList = new List<DeveloperDto>();
        
        
        foreach (var developer in developers)
        {
            DeveloperDto DeveloperDto = new()
            {
                Id = developer.Id,
                Nickname = developer.Nickname,
            };
            developersList.Add(DeveloperDto);
        }
        
        
        return developersList;
    }


    public async Task<DeveloperDto> GetById(Guid id)
    {
        Developer? developer = await context.Developers.FindAsync(id);


        return new DeveloperDto()
        {
            Id = developer.Id,
            Nickname = developer.Nickname
        };
    }


    public async Task<DeveloperDto> Create(CreateDeveloperDto developer)
    {
        Developer createdDeveloper = new()
        {
            Nickname = developer.Nickname,
        };
        
        
        context.Developers.Add(createdDeveloper);
        await context.SaveChangesAsync();


        return new DeveloperDto()
        {
            Id = createdDeveloper.Id,
            Nickname = createdDeveloper.Nickname
        };
    }


    public async Task<DeveloperDto> Update(UpdateDeveloperDto developer)
    {
        Developer? updatedDeveloper = await context.Developers.FindAsync(developer.Id);
        
        
        updatedDeveloper.Nickname = developer.Nickname;
        
        
        context.Developers.Update(updatedDeveloper);
        await context.SaveChangesAsync();


        return new DeveloperDto()
        {
            Id = updatedDeveloper.Id,
            Nickname = updatedDeveloper.Nickname,
        };
    }


    public async Task Delete(Guid id)
    {
        Developer? developer = await context.Developers.FindAsync(id);
        context.Developers.Remove(developer);
        await context.SaveChangesAsync();
    }
}