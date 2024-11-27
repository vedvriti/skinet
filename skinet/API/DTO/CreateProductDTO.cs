using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class CreateProductDTO
{
    [Required]
   public  string Name { get; set; } = string.Empty;
   [Required]
   public  string Description { get; set; } = string.Empty;
   [Range(0.01,double.MaxValue,ErrorMessage ="Prize must be greater than 0")]
   public decimal price { get; set; }
   [Required]
   public  string PictureUrl { get; set; }
   [Required]
   public  string Type { get; set; }
   [Required]
   public  string Brand { get; set; }
   [Range(1,int.MaxValue,ErrorMessage ="Quantity must be atleast 1")]
   public int QuantityStock { get; set; }
}
