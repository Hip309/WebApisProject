using AutoMapper;
using Microsoft.Build.Framework.Profiler;
using StoreApi.Entities;
using StoreApi.Models;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;

namespace StoreApi.Helper
{
    public class AppMapper: Profile
    {
        public AppMapper() { 
            //User
            CreateMap<User, UserRespond>().ReverseMap();
            CreateMap<UserRequest,User>().ReverseMap();
            CreateMap<UserRequest, UserRespond>().ReverseMap();
            //Category
            CreateMap<CategoryRequest, CategoryRespond>().ReverseMap(); 
            CreateMap<Category, CategoryRespond>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            //Product
            CreateMap<ProductRequest, ProductRespond>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<Product, ProductRespond>().ReverseMap();
        }
    }
}
