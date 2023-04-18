using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models.RspondModels
{
    public class UserRespond:RespondModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
