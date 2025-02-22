using System.Linq.Expressions;
using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Persistence;
using MeetingRoomBookingSystem.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBookingSystem.Core.Repositories;

public class GeneralRepository<T> : IGeneralRepository<T> where T : ModelBase
{
    private readonly MrbsDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GeneralRepository(MrbsDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, bool includeDeletedItems = false)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> PagingGetAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, bool includeDeletedItems = false)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = query.OrderByDescending(x => x.CreatedAt).Skip(pageIndex * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public void Delete(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
