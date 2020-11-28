namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;

    public class ProfileController : BaseController
    {
        private readonly IFriendService friendService;

        public ProfileController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        public IActionResult Details()
        {
            var viewModel = this.friendService.All();
            return this.View(viewModel);
        }
    }
}
