using System;
using Core.Entities;

namespace Core.Specifications;
//Product specification effectively derived from the base specification so we have got access to the properties and methods inside the class
public class ProductSpecification : BaseSpecification<Product>
{
    //Build up our ecpression the where clause in the base constructor
    public ProductSpecification(string? brand, string? type,string? sort):base(x =>
    (string.IsNullOrWhiteSpace(brand)|| x.Brand == brand) &&
    (string.IsNullOrWhiteSpace(type)|| x.Type == type))
    {

       switch(sort)
       {
          case "priceAsc":
              AddOrderBy(x =>x.price);
              break;
          case "priceDesc":
              AddOrderByDescending(x => x.price);
              break;
          default:
              AddOrderBy(x => x.Name);
              break;
       }

    }

}
