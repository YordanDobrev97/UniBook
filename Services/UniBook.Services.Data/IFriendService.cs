namespace UniBook.Services.Data
{
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels.Friends;

    public interface IFriendService
    {
        ProfileDetailsViewModel All(string userId);

        string SendRequest(string senderId, string username);

        bool IsSendRequestFriendship(string userId, string username);

        bool IsAlreadyFriend(string userId, string username);

        void Accept(string senderId, string reciverId);

        void UpdateStatus(string senderId, string reciverId);
    }
}
