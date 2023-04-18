namespace StoreApi.Models.RspondModels
{
    public class CategoryRespondList:RespondListModel<CategoryRespond>
    {
        public CategoryRespondList() {
            ResponseMessage = "List of Category response";
            StatusCode = 200;
        }
    }
}
