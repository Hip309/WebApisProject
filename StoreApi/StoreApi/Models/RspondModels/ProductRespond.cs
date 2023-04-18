namespace StoreApi.Models.RspondModels
{
    public class ProductRespond:RespondModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; } 
        public double Price { get; set; }
        public double Cost { get; set; } 
        public int Quantity { get; set; }
        public string? Image { get; set; } 
        public int CategoryId { get; set; } 
    }
}
