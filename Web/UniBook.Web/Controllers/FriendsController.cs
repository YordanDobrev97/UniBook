namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using UniBook.Services.Data;

    public class FriendsController : BaseController
    {
        private readonly IUsersService usersService;

        public FriendsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var users = this.usersService.All(userId);
            return this.View(users);
        }
    }
}
