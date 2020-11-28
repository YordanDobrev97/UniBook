namespace UniBook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Friends;

    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext db;

        public FriendService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ProfileDetailsViewModel All()
        {
            var friends = this.db.UserFriends
                .Select(e => new ListUsersViewModel
                {
                    Username = e.Receiver.UserName,
                }).ToList();

            var viewModel = new ProfileDetailsViewModel
            {
                Friends = friends,
            };

            return viewModel;
        }

        public bool IsFriends(string userId, string username)
        {
            var user = this.FindReciver(username);
            return this.db.UserFriendRequests
                .Any(e => e.Sender.Id == userId && e.Receiver.Id == user.Id);
        }

        public string SendRequest(string senderId, string username)
        {
            ApplicationUser reciver = this.FindReciver(username);

            if (reciver == null)
            {
                return null;
            }

            this.db.UserFriendRequests.Add(new FriendRequest
            {
                SenderId = senderId,
                Receiver = reciver,
            });

            this.db.SaveChanges();

            return reciver.Id;
        }

        private ApplicationUser FindReciver(string username)
        {
            return this.db.Users.FirstOrDefault(e => e.UserName == username);
        }
    }
}
