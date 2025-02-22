using System.Linq.Expressions;

namespace MeetingRoomBookingSystem.Core.IRepositories;

public interface IGeneralRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, bool includeDeletedItems = false);

    Task<IEnumerable<T>> PagingGetAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, bool includeDeletedItems = false);

    Task InsertAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveAsync();
}
