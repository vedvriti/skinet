using System;

namespace Core.Specifications;

public class ProductSpecParams
{
  //Max page size a client can request
   private const int MaxPageSize = 50;
   public int PageIndex {get;set;} = 1;
   private int _pagesize = 6;
   public int PageSize
   {
    get => _pagesize;
    set => _pagesize = (value > MaxPageSize) ? MaxPageSize : value;
    
   }
   
    //Declaring properties brands and types
    //Common design patter - BACKING FIELDS IN C# 
    // backing fields in C#. This approach separates the private field and its corresponding public property 
    // private field holds the actual data 
    // public property controls how that data is accessed or modified.
  private List<string> _brands = [];
  public List<string> Brands
  {
    get => _brands; //type = gloves, boards
    set
    {
        _brands = value.SelectMany(x=> x.Split(','
        ,StringSplitOptions.RemoveEmptyEntries)).ToList();
    }
  }

  //If value contains items like ["Nike,Adidas", "Puma"], the _brands field will be set to a list like ["Nike", "Adidas", "Puma"].

  private List<string> _types = [];
  public List<string> Types
  {
    get => _types; //type = gloves, boards
    set
    {
        _types = value.SelectMany(x=> x.Split(','
        ,StringSplitOptions.RemoveEmptyEntries)).ToList();
    }
  }

      public string? Sort {get;set;}

  
}



