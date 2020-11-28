namespace UniBook.Services.Data
{
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels.Friends;

    public interface IFriendService
    {
        ProfileDetailsViewModel All();

        string SendRequest(string senderId, string username);

        bool IsFriends(string userId, string username);
    }
}
