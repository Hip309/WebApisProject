namespace StoreMVC.Client.Models.Response
{
    public class UserResponseList
    {
        public List<UserResponse>? RespondList { get; set; }
        public int PageCurrent { get; set; } = 0;
        public int PageSize { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string? ResponseMessage { get; set; } = "List of Users";
        public int StatusCode { get; set; } = 204;
    }
}
