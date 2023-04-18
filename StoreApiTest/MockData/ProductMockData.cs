using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi_Test.MockData
{
    public class ProductMockData
    {

        public static List<ProductRespond> GetProductsData()
        {
            return new List<ProductRespond>()
            {
                new ProductRespond() {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Powdered Milk",
                    Unit = "Box",
                    Price = 155000,
                    Cost = 150000,
                    Quantity = 24,
                    Image = "image",
                    CategoryId = 1
                },
                new ProductRespond() {
                    Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Toma Snack",
                    Unit = "Bag",
                    Price = 25000,
                    Cost = 19000,
                    Quantity = 37,
                    Image = "image",
                    CategoryId = 2
                },
                new ProductRespond() {
                    Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Lemon Candy",
                    Unit = "Pack",
                    Price = 12000,
                    Cost = 8000,
                    Quantity = 59,
                    Image = "image",
                    CategoryId = 3
                }
            };
        }

        public static List<ProductRespond> GetProductsOfCategory()
        {
            return new List<ProductRespond>()
            {
                new ProductRespond() {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Powdered Milk",
                    Unit = "Box",
                    Price = 155000,
                    Cost = 150000,
                    Quantity = 24,
                    Image = "image",
                    CategoryId = 1
                },
                new ProductRespond() {
                    Id = new Guid("c86a675a-6ccb-48b4-829b-eb44b7fe0e60"),
                    Name = "Condensed Milk",
                    Unit = "Can",
                    Price = 18000,
                    Cost = 16000,
                    Quantity = 9,
                    Image = "image",
                    CategoryId = 1
                },
                new ProductRespond() {
                    Id = new Guid("9b240ca3-e18d-452a-841b-d966f1ddd251"),
                    Name = "Fresh Milk",
                    Unit = "Bottle",
                    Price = 12000,
                    Cost = 8000,
                    Quantity = 36,
                    Image = "image",
                    CategoryId = 1
                }
            };
        }


        public static ProductRequest GetProductRequest()
        {
            return new ProductRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Lay Snack",
                Unit = "Bag",
                Price = 23000,
                Cost = 198000,
                Quantity = 18,
                Image = "image",
                CategoryId = 2
            };
        }

        public static ProductRespond GetProductRespond()
        {
            return new ProductRespond()
            {
                Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                Name = "Toma Snack",
                Unit = "Bag",
                Price = 25000,
                Cost = 19000,
                Quantity = 37,
                Image = "image",
                CategoryId = 2
            };
        }

    }
}
