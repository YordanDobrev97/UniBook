namespace UniBook.Services.Data
{
    using System;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Books;

    public class UsersService : IUsersService
    {
        private const int InvalidId = 0;
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool SaveBookPage(ReadBookViewModel bookViewModel)
        {
            string userId = bookViewModel.UserId;
            int bookId = bookViewModel.BookId;

            if (userId == null || bookId == InvalidId)
            {
                return false;
            }

            var userBook = this.db.UserBooks
                .FirstOrDefault(e => e.UserId == userId && e.BookId == bookId);

            if (userBook == null)
            {
                this.db.UserBooks.Add(new UserBook
                {
                    UserId = userId,
                    BookId = bookId,
                    CreatedOn = DateTime.UtcNow,
                    ReadCount = bookViewModel.ReadCount,
                });
            }
            else
            {
                userBook.ReadCount = bookViewModel.ReadCount;
            }

            this.db.SaveChanges();
            return true;
        }

        public void VoteBook(VoteBookViewModel bookViewModel)
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
        }

        public bool IsStartReadBook(string userId, int bookId)
        {
            return this.db.UserBooks.Any(e => e.UserId == userId && e.BookId == bookId);
        }

        public ContentBookViewModel GetStartReadBook(string userId, int bookId)
        {
            var book = this.db.UserBooks
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .Select(e => new ContentBookViewModel
                {
                    ReadCount = e.ReadCount,
                    Title = e.Book.Name,
                    Content = e.Book.Body,
                    UserId = e.UserId,
                    BookId = e.BookId,
                }).FirstOrDefault();

            return book;
        }
    }
}
