using System;

namespace Core.Entities;
// Core - It contains the business entities
public class Product : BaseEntity
{
   //required keyword used is not for validation - It will restrict us for creating a product if any of th field is empty
   //required - accomodate nullable reference type
   public required string Name { get; set; }
   public required string Description { get; set; }
   public decimal price { get; set; }
   public required string PictureUrl { get; set; }
   public required string Type { get; set; }
   public required string Brand { get; set; }
   public int QuantityStock { get; set; }

}

