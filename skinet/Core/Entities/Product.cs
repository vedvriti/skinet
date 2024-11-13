using System;

namespace Core.Entities;
// Core - It contains the business entities
public class Product : BaseEntity
{
   public required string Name { get; set; }
   public required string Description { get; set; }
   public decimal price { get; set; }
   public required string PictureUrl { get; set; }
   public required string Type { get; set; }
   public required string Brand { get; set; }
   public int QuantityStock { get; set; }

}

