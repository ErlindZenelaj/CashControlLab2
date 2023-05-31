namespace Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<List<T>> GetAsync();

        void Insert(T entity);

        void InsertMany(IEnumerable<T> entity);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> SaveChangesAsync();

        bool SaveChanges();
    }
}