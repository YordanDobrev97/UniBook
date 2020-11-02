namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
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
                this.userBookService.SaveStartRead(value.BookId);
                //TODO fix working with service

                //this.userBookService.SaveAsync(value.BookId, value.UserId, value.ReadCount);
            }

            return new JsonResult("Okey");
        }

        [HttpPost("api/[controller]/Book")]
        public IActionResult VoteBook([FromBody] VoteBookViewModel bookViewModel)
        {
            var bookVote = this.db.BookVotes
                .FirstOrDefault(x => x.BookId == bookViewModel.BookId
                                && x.UserId == bookViewModel.UserId);

            if (bookVote == null)
            {
                var book = this.db.Books.FirstOrDefault(x => x.Id == bookViewModel.BookId);

                book.Votes++;

                this.db.BookVotes.Add(new BookVotes
                {
                    UserId = bookViewModel.UserId,
                    BookId = bookViewModel.BookId,
                    IsVoting = true,
                });
                this.db.SaveChanges();
            }

            return new JsonResult("Ok");
        }
    }
}
