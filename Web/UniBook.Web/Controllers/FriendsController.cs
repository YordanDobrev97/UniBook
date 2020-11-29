namespace UniBook.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Friends;

    public class FriendsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IFriendService friendService;

        public FriendsController(
            IUsersService usersService,
            IFriendService friendService)
        {
            this.usersService = usersService;
            this.friendService = friendService;
        }

        public IActionResult Index()
        {
            string userId = this.UserId();
            var users = this.usersService.All(userId);

            foreach (var user in users)
            {
                var isAdded = this.friendService.IsAlreadyFriend(userId, user.Username);

                if (isAdded)
                {
                    user.IsAlreadyFriend = true;
                    continue;
                }

                var isSendRequestFriend = this.friendService.IsSendRequestFriendship(userId, user.Username);
                if (isSendRequestFriend)
                {
                    user.IsSendRequest = true;
                }
            }

            return this.View(users);
        }

        [HttpPost]
        public IActionResult RequestFriend(RequestFriendViewModel input)
        {
            var userId = this.UserId();
            string reciverId = this.friendService.SendRequest(userId, input.Username);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Accept(AcceptFriendshipRequest input)
        {
            var userId = this.UserId();
            this.friendService.Accept(userId, input.Username);
            this.friendService.UpdateStatus(input.Username, userId);
            return this.RedirectToAction("Details", "Profile");
        }

        private string UserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
