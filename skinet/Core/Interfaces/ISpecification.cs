using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    //Methods we are going to support via specification critera == where specification where we are filter and retrieve the data from database
  Expression<Func<T, bool>>? Criteria { get; }
  Expression<Func<T, object>>? OrderBy {get;}
  //As we dont know what will be the return type it can either be string, decimal or anything so we made the return type as object
  Expression<Func<T, object>>? OrderByDescending {get;}
}
