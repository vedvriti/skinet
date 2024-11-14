using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository //Primary constructor
{
//    private readonly StoreContext context;
//    public ProductRepository(StoreContext context)
//    {
//       this.context = context;   
//    }
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
       context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandAsync()
    {
        //Select the distinct brands
       return await context.Products.Select(x => x.Brand)
       .Distinct()
       .ToListAsync();
    }

  

    public async Task<IReadOnlyList<Product>> GetProductAsync(string? brand,string? type,string? sort)
    {
        //return await context.Products.ToListAsync();
        //AsQueryable allows flexible query building, enabling the application of filters and sorting conditions later.
        var query = context.Products.AsQueryable();

        //Checks if the brand parameter is not null, empty, or whitespace.
        if(!string.IsNullOrWhiteSpace(brand))
        //filters query to include only products where the Brand property matches the given brand.
          query = query.Where(x => x.Brand==brand);

        if(!string.IsNullOrWhiteSpace(type))
          query = query.Where(x => x.Type==type);

        query = sort switch
        {    
            //Sorts query by price in ascending order
            "priceAsc" => query.OrderBy(x=>x.price),
            //Sorts query by price in descending order
            "priceDesc" => query.OrderByDescending(x=>x.price),
            // If sort is null or any other value, it sorts query by Name in ascending order
            _ => query.OrderBy(x=>x.Name)

        };
        //Asynchronously executes the query and returns the results as a list of Product objects. ToListAsync fetches the data from the database, executing the query and returning the filtered, sorted list as an asynchronous result.
        return await query.ToListAsync();
    }


    public async Task<Product?> GetProductByIdAsync(int id)
    {
       return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
       return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
