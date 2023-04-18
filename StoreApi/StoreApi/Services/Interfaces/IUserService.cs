using StoreApi.Entities;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;

namespace StoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRespond> CreateUser(UserRequest uRequest);

        UserRespondList GetAllUsers(int pageIndex, int pageSize);

        Task<UserRespond> GetUserById(int UserId);

        Task<UserRespond> UpdateUser(UserRequest uRequest);

        Task<UserRespond> DeleteUser(int UserId);
    }
}
