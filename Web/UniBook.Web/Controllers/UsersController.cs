namespace UniBook.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Books;

    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("api/[controller]/BookData")]
        public IActionResult Save([FromBody] ReadBookViewModel value)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.usersService.SaveBookPage(value, userId);
            return new JsonResult("Ok");
        }

        [HttpPost("api/[controller]/Book")]
        public IActionResult VoteBook([FromBody] VoteBookViewModel bookViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.usersService.VoteBook(bookViewModel, userId);
            return new JsonResult("Ok");
        }

        [HttpPost("api/[controller]/AddToReadedBooks")]
        public IActionResult AddToReadedBooks([FromBody] ReadBookViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.usersService.AddToReadedBooks(viewModel, userId);
            return new JsonResult("Ok");
        }
    }
}
