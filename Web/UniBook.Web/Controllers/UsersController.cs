namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels;

    public class UsersController : ControllerBase
    {
        private readonly IUserBookService userBookService;
        private readonly ApplicationDbContext db;

        public UsersController(IUserBookService userBookService, ApplicationDbContext db)
        {
            this.userBookService = userBookService;
            this.db = db;
        }

        [HttpPost]
        [Route("api/[controller]/BookData")]
        public IActionResult Save([FromBody] ReadBookViewModel value)
        {
            if (this.ModelState.IsValid)
            {
                this.db.UserBooks.Add(new UserBook
                {
                    BookId = value.BookId,
                    UserId = value.UserId,
                    ReadCount = value.ReadCount,
                });
                this.db.SaveChanges();
                //this.userBookService.SaveAsync(value.BookId, value.UserId, value.ReadCount);
            }

            return new JsonResult("Okey");
        }
    }
}
