using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specifications;

public abstract class Specification<T> where T : class
{
    private List<Func<IQueryable<T>, IQueryable<T>>> Includes = new();
    private List<Expression<Func<T, bool>>> Criteria { get; } = new();

    protected void AddInclude(Func<IQueryable<T>, IQueryable<T>> include)
    {
        Includes.Add(include);
    }

    protected void AddIncludeIf(bool condition,Func<IQueryable<T>, IQueryable<T>> include)
    {
        if (condition)
        {
            Includes.Add(include);
        }
    }
    
    protected void AddCriteriaIf(
        bool condition, 
        Expression<Func<T, bool>> criteria)
    {
        if (condition)
        {
            Criteria.Add(criteria);
        }
    }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria.Add(criteria);
    }

    public virtual IQueryable<T> Apply(IQueryable<T> query)
    {
        query = Includes.Aggregate(query, 
            (current, include) => include(current));
        
        return Criteria.Aggregate(query, 
            (current, criteria) => current.Where(criteria));
    }
}