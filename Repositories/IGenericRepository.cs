using System.Linq.Expressions;

namespace QuanLyThuVien.Repositories
{
    // Interface chung cho các thao tác CRUD cơ bản, áp dụng cho mọi Entity
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
