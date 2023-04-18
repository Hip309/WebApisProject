namespace StoreApi.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll(int pageIndex, int pageSize);
        Task<T> GetById(int id);
        Task<T> GetById(Guid Id);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
