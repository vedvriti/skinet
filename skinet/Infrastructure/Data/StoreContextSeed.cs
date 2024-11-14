using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        //seeds initial data into the StoreContext database context if no products currently exist.
        if(!context.Products.Any())
        {
            //Read the contents of file products.json
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            //Deserializes the JSON data from productsData into a list of Product objects. It converts the JSON text into C# objects
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if(products == null) return;
            // Adds the list of Product objects to the Products table in the context (database)
            //AddRange allows multiple entities to be added in a single call, optimizing the seeding process.
            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }

}
