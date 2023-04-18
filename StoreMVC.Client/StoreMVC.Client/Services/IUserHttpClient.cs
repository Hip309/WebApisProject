namespace StoreMVC.Client.Services
{
    public interface IUserHttpClient
    {
        Task<HttpClient> GetClient();
    }
}
