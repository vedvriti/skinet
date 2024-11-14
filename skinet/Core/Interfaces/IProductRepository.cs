using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
     Task<IReadOnlyList<Product>> GetProductAsync(string? brand,string? type,string? sort);
     Task<Product?> GetProductByIdAsync(int id);
     Task<IReadOnlyList<string>> GetBrandAsync(); //List the brands
     Task<IReadOnlyList<string>> GetTypesAsync();
     void AddProduct(Product product);
     void UpdateProduct(Product product);
     void DeleteProduct(Product product);
     bool ProductExists(int id);
     Task<bool> SaveChangesAsync(); // To see what are the changes made in the database


}
