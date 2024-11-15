using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    //Methods we are going to support via specification critera == where specification where we are filter and retrieve the data from database
  Expression<Func<T, bool>> Criteria { get; }
}
