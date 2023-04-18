namespace StoreApi.Models.RequestModels
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = "string";
        public string Email { get; set; } = "string";
        public string PhoneNumber { get; set; } = "string";
        public string Password { get; set; } = "string"; 
    }
}
