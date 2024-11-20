using System;
using Core.Entities;

namespace Core.Specifications;
//Product specification effectively derived from the base specification so we have got access to the properties and methods inside the class
public class ProductSpecification : BaseSpecification<Product>
{
    //Build up our ecpression the where clause in the base constructor
    // public ProductSpecification(string? brand, string? type,string? sort):base(x =>
    // (string.IsNullOrWhiteSpace(brand)|| x.Brand == brand) &&
    // (string.IsNullOrWhiteSpace(type)|| x.Type == type))
    // {

    //    switch(sort)
    //    {
    //       case "priceAsc":
    //           AddOrderBy(x =>x.price);
    //           break;
    //       case "priceDesc":
    //           AddOrderByDescending(x => x.price);
    //           break;
    //       default:
    //           AddOrderBy(x => x.Name);
    //           break;
    //    }

    //filtering criteria
    public ProductSpecification(ProductSpecParams specParams):base(x =>
    (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
    (specParams.Brands.Count == 0|| specParams.Brands.Contains(x.Brand)) &&
    (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type)))
    {

      ApplyPaging(specParams.PageSize * (specParams.PageIndex -1),specParams.PageSize);//Passing the skip and take
    //    ApplyPaging(
    //                Math.Max(0, specParams.PageSize * (specParams.PageIndex - 1)), 
    //                Math.Max(1, specParams.PageSize)
    //              );

       switch(specParams.Sort)
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
