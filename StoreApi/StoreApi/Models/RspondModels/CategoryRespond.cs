using StoreApi.Entities;

namespace StoreApi.Models.RspondModels
{
    public class CategoryRespond:RespondModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
