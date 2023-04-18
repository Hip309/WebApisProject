namespace StoreMVC.Client.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ResponseMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
