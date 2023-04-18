using StoreApi.Entities;

namespace StoreApi.Models.RspondModels
{
    public class UserRespondList:RespondListModel<UserRespond>
    {
        public UserRespondList() {
            ResponseMessage = "List of User response";
            StatusCode= 200;
        }
    }
}
