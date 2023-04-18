using AutoMapper;
using StoreApi.Entities;
using StoreApi.IRepositories;
using StoreApi.Logger;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;

namespace StoreApi.Services
{
    public class UserService:IUserService 
    {
        private readonly IMapper _mapper;
        public readonly ILoggerService _logger;
        public IUnitOfWork _unitOfwork;

        public UserService(IUnitOfWork unitOfwork, IMapper mapper, ILoggerService logger)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserRespond> CreateUser(UserRequest uRequest)
        {
            UserRespond uRespond = new UserRespond();
            if (uRequest != null) {
                _logger.LogInfo("Inserting new User into database");
                var newUser = _mapper.Map<User>(uRequest); 
                await _unitOfwork.Users.Add(newUser);
                var result = _unitOfwork.Save();
                if(result > 0)
                {
                    _logger.LogInfo("Create new user success");
                    uRespond = _mapper.Map<UserRespond>(newUser);
                    uRespond.StatusCode = 200;
                    uRespond.ResponseMessage = "Create new user success";
                    return uRespond;
                }
                else
                {
                    uRespond.StatusCode = 500;
                    uRespond.ResponseMessage = "Create User method went wrong";
                    return uRespond;
                }
            }
            _logger.LogWarn("Request is wrong");
            uRespond.StatusCode = 400;
            uRespond.ResponseMessage = "Request is wrong";
            return uRespond;
        }

        public async Task<UserRespond> DeleteUser(int UserId)
        {
            UserRespond uRespond = new UserRespond();
            if(UserId > 0)
            {
                _logger.LogInfo("Searching the detele user in database");
                var deleteUser = await _unitOfwork.Users.GetById(UserId);
                if(deleteUser != null)
                {
                    _logger.LogInfo("Deleting user");
                    _unitOfwork.Users.Delete(deleteUser);
                    var result = _unitOfwork.Save();

                    if(result > 0)
                    {
                        _logger.LogInfo("Delete user success");
                        uRespond = _mapper.Map<UserRespond>(deleteUser);
                        uRespond.StatusCode = 200;
                        uRespond.ResponseMessage = "Delete user success";
                        return uRespond;
                    }
                    else {
                        uRespond.StatusCode = 500;
                        uRespond.ResponseMessage = "Delete User method went wrong";
                        return uRespond;
                    }
                }
            }
            _logger.LogWarn("User is not exist");
            uRespond.StatusCode = 404;
            uRespond.ResponseMessage = "User is not exist";
            return uRespond;
        }

        public UserRespondList GetAllUsers(int pageIndex, int pageSize)
        {
            _logger.LogInfo("Fetching All Users");
            var userList =  _unitOfwork.Users.GetAll(pageIndex, pageSize);
            var respondList = new UserRespondList();
            respondList.RespondList = _mapper.Map<List<UserRespond>>(userList);
            respondList.PageCurrent = pageIndex;
            respondList.PageSize = pageSize;
            respondList.PageCount = userList.Count;
            if (userList == null)
            {
                _logger.LogWarn("No user is exist in database");
                respondList.StatusCode = 204;
            }
            _logger.LogInfo($"Total User responses: {respondList.PageCount}");
            return  respondList;
        }

        public async Task<UserRespond> GetUserById(int UserId)
        {
            UserRespond uRespond = new UserRespond();
            _logger.LogInfo("Searching user in database");
            var user = await _unitOfwork.Users.GetById(UserId);
            if(user != null)
            {
                _logger.LogInfo("Find out user in database");
                uRespond = _mapper.Map<UserRespond>(user);
                uRespond.StatusCode = 200;
                uRespond.ResponseMessage = "Get user success";
                return uRespond;
            }
            _logger.LogWarn("Can not find user in database");
            uRespond.StatusCode = 404;
            uRespond.ResponseMessage = "User is not exist";
            return uRespond;
        }

        public async Task<UserRespond> UpdateUser(UserRequest uRequest)
        {
            UserRespond uRespond = new UserRespond();
            if (uRequest != null)
            {
                _logger.LogInfo("Searching the update user in database");
                var existedUser = await _unitOfwork.Users.GetById(uRequest.Id);
                if(existedUser != null)
                {
                    existedUser.Name = uRequest.Name;
                    existedUser.Email = uRequest.Email;
                    existedUser.PhoneNumber = uRequest.PhoneNumber;
                    existedUser.Password = uRequest.Password;

                    _logger.LogInfo("Updating user");
                    _unitOfwork.Users.Update(existedUser);
                    var result = _unitOfwork.Save();
                    if (result > 0)
                    {
                        _logger.LogInfo("Update user success");
                        uRespond = _mapper.Map<UserRespond>(existedUser);
                        uRespond.StatusCode = 200;
                        uRespond.ResponseMessage = "Update user success";
                        return uRespond;
                    }
                    else {
                        uRespond = _mapper.Map<UserRespond>(uRequest);
                        uRespond.StatusCode = 500;
                        uRespond.ResponseMessage = "Update user method went wrong";
                        return uRespond;
                    }
                }
            }
            _logger.LogWarn("Can not update user in");
            uRespond.StatusCode = 400;
            uRespond.ResponseMessage = "Request is wrong";
            return uRespond;
        }
    }
}
