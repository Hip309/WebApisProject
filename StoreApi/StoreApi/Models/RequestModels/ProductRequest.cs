using StoreApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models.RequestModels
{
    public class ProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "string";
        public string Unit { get; set; } = "string";
        public double Price { get; set; } = 0.0;
        public double Cost { get; set; } = 0.0;
        public int Quantity { get; set; } = 0;
        public string Image { get; set; } = "string";
        public int CategoryId { get; set; } = 0;
    }
}
