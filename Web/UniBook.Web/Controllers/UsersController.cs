namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Books;

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

            this.usersService.SaveBookPage(value);
            return new JsonResult("Ok");
        }

        [HttpPost("api/[controller]/Book")]
        public IActionResult VoteBook([FromBody] VoteBookViewModel bookViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            this.usersService.VoteBook(bookViewModel);
            return new JsonResult("Ok");
        }
    }
}
