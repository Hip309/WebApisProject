using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi_Test.MockData
{
    public class UserMockData
    {
        public static List<UserRespond> GetUsersData()
        {

            return new List<UserRespond>()
            {
                new UserRespond{Id = 1, Email = "admin@email.com", Name = "Admin", PhoneNumber = "011892893145"},
                new UserRespond{Id = 2, Email = "user01@mail.com", Name= "User01", PhoneNumber = "021113114115"},
                new UserRespond{Id = 3, Email = "user02@mail.com", Name= "User02", PhoneNumber = "031241250063"}
            };

        }

        public static UserRequest GetUserRequest() 
        {
            return new UserRequest()
            {
                Id = 0,
                Email = "admin2@email.com",
                Name = "Admin02",
                PhoneNumber = "0112233456",
                Password = "admin2@123"
            };
        }

        public static UserRespond GetUserRespond()
        {
            return new UserRespond()
            {
                Id = 1,
                Email = "admin@email.com",
                Name = "Admin",
                PhoneNumber = "011892893145"
            };
        }
    }
}
