using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi_Test.MockData
{
    public class CategoryMockData
    {
        public static List<CategoryRespond> GetCategoriesData() 
        {
            return new List<CategoryRespond>()
            {
                new CategoryRespond() { Id = 1, Name = "Milk", Image = "image"},
                new CategoryRespond() { Id = 2, Name = "Snack", Image = "image"},
                new CategoryRespond() { Id = 3, Name = "Candy", Image = "image"}
            };
        }

        public static CategoryRequest GetCategoryRequest()
        {
            return new CategoryRequest() { Id = 0, Name = "Wipes", Image = "image" };
        }

        public static CategoryRespond GetCategoryRespond()
        {
            return new CategoryRespond() { Id = 1, Name = "Milk", Image = "image" };
        }
    }
}
