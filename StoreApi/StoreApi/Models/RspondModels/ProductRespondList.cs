namespace StoreApi.Models.RspondModels
{
    public class ProductRespondList:RespondListModel<ProductRespond>
    {
        public ProductRespondList()
        {
            ResponseMessage = "List of Product response";
            StatusCode = 200;
        }
    }
}
