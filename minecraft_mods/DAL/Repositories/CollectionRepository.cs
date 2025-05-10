using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Collection;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class CollectionRepository(ApplicationContext context) : IRepository<CollectionDto, CreateCollectionDto, UpdateCollectionDto>
{
    public async Task<List<CollectionDto>> GetAll()
    {
        List<Collection> collections = await context.Collections.ToListAsync();
        List<CollectionDto> collectionsList = new List<CollectionDto>();


        foreach (var collection in collections)
        {
            CollectionDto collectionDto = new()
            {
                Id = collection.Id,
                Name = collection.Name,
                TimeToComplete = collection.TimeToComplete
            };
            collectionsList.Add(collectionDto);
        }


        return collectionsList;
    }


    public async Task<CollectionDto> GetById(Guid id)
    {
        Collection? collection = await context.Collections.FindAsync(id);


        return new CollectionDto()
        {
            Id = collection.Id,
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete
        };
    }


    public async Task<CollectionDto> Create(CreateCollectionDto collection)
    {
        Collection createdCollection = new()
        {
            Name = collection.Name,
            TimeToComplete = collection.TimeToComplete
        };


        context.Collections.Add(createdCollection);
        await context.SaveChangesAsync();


        return new CollectionDto()
        {
            Id = createdCollection.Id,
            Name = createdCollection.Name,
            TimeToComplete = createdCollection.TimeToComplete
        };
    }


    public async Task<CollectionDto> Update(UpdateCollectionDto collection)
    {
        Collection? updatedCollection = await context.Collections.FindAsync(collection.Id);


        updatedCollection.Name = collection.Name;
        updatedCollection.TimeToComplete = collection.TimeToComplete;



        context.Collections.Update(updatedCollection);
        await context.SaveChangesAsync();


        return new CollectionDto()
        {
            Id = updatedCollection.Id,
            Name = updatedCollection.Name,
            TimeToComplete = updatedCollection.TimeToComplete
        };
    }


    public async Task Delete(Guid id)
    {
        Collection? collection = await context.Collections.FindAsync(id);
        context.Collections.Remove(collection);
        await context.SaveChangesAsync();
    }
}
