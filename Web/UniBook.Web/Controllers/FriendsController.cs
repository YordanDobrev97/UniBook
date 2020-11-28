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

        public IActionResult Index(string reciverId)
        {
            string userId = this.UserId();
            var users = this.usersService.All(userId);

            foreach (var user in users)
            {
                var isFriend = this.friendService.IsFriends(userId, user.Username);
                if (isFriend)
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
            return this.RedirectToAction("Index", new { reciverId = reciverId});
        }

        private string UserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
