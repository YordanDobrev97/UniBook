namespace UniBook.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Friends;

    public class ProfileController : BaseController
    {
        private readonly IFriendService friendService;

        public ProfileController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        public IActionResult Details()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var viewModel = this.friendService.All(userId);
            return this.View(viewModel);
        }

        public IActionResult Chat(string id)
        {
            var viewModel = new ChatViewModel
            {
                UserId = id,
            };

            return this.View(viewModel);
        }
    }
}
