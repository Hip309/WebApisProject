using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StoreApi.Models.RspondModels
{
    public abstract class RespondListModel<T>
    {
        public List<T>? RespondList { get; set; }
        public int PageCurrent { get; set; } = 0;
        public int PageSize { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string? ResponseMessage { get; set; } = "List of items";
        public int StatusCode { get; set; } = 204;
    }
}
