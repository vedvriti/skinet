using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
//Inject Store Context class - The constructor parameter StoreContext context is injected to access the database.
// A generic constraint that specifies T must inherit from BaseEntity. This ensures that T has properties from BaseEntity, like an Id property.
public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        //Set the entity as there is only one entity we have created so we can determine it is a product enetity but the compiler cannot detrmine
        //context.Set<T>() returns a set of T entities from the StoreContext.
        context.Set<T>().Add(entity);
    }

    public bool Exists(int id)
    {
        //Queries the database for any T entity where Id matches the id parameter. Any returns true if a match is found, otherwise false.
        return context.Set<T>().Any(x=>x.Id == id);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        //Finds an entity with the specified id asynchronously and returns it. Returns null if no match is found.
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        //retrieves all T entities as a read-only list.
        return await context.Set<T>().ToListAsync();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        //Saves all pending changes and returns true if any rows were affected.
        return await context.SaveChangesAsync()> 0;
    }

    public void Update(T entity)
    {
        //Attaches the entity to the context, so changes can be tracked.
        context.Set<T>().Attach(entity);
        //Marks the entity as Modified, so it will be updated on the next SaveChanges call.
        context.Entry(entity).State = EntityState.Modified;
    }
}