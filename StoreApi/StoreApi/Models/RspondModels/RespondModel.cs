namespace StoreApi.Models.RspondModels
{
    public abstract class RespondModel
    {
        public string? ResponseMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
