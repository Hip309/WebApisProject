namespace StoreApi.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        int Save();
    }
}
