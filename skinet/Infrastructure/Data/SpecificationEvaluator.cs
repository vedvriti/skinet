using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T :BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        //Without Projection

        //Actual query that is going to database
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);// x => x.Brand == brand
        }

        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if(spec.IsDistinct)
        {
            query = query.Distinct();
        }

        if(spec.IsPagingEnabled && spec.Skip >= 0 && spec.Take > 0)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        return query;
    }

    //With projection
    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {

        //Actual query that is going to database
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);// x => x.Brand == brand
        }

        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        var selectQuery = query as IQueryable<TResult>;
        if(spec.select != null)
        {
            selectQuery = query.Select(spec.select);
        }  
        if(spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        if(spec.IsPagingEnabled && spec.Skip >= 0 && spec.Take > 0)
        {
            selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
        }
       // The null-coalescing operator is used to return the left-hand operand if it is not null; otherwise, it returns the right-hand operand.
        return selectQuery ?? query.Cast<TResult>();  
    }
}
