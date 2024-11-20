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
         //Pass thr brands that we wish to filter by as a query string but comma separated values, Separting the comma separated value as a list of string using the method split
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

      private string? _search;
      public string Search
      {
        //The null coalescing operator ?? checks if the left-hand side (_search) is null. If _search is null, it returns the right-hand side value ("" in this case). If _search is not null, it simply returns _search.
        get => _search ?? "";
        set => _search = value.ToLower();
      }
      
  
}



