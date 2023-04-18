using AutoMapper;
using StoreMVC.Client.Models;
using StoreMVC.Client.Models.Response;

namespace StoreMVC.Client.Helper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            //User
            CreateMap<UserResponse, UserViewModel>().ReverseMap();
        }
    }
}
