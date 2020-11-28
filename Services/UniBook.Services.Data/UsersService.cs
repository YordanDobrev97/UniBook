namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Books;
    using UniBook.Web.ViewModels.Friends;

    public class UsersService : IUsersService
    {
        private const int InvalidId = 0;
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ListUsersViewModel> All(string userId)
        {
            var users = this.db.Users
                .Where(e => e.Id != userId)
                .Select(e => new ListUsersViewModel
                {
                    Username = e.UserName,
                }).ToList();

            return users;
        }

        public bool SaveBookPage(ReadBookViewModel bookViewModel, string userId)
        {
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

        public void VoteBook(VoteBookViewModel bookViewModel, string userId)
        {
            var bookVote = this.db.BookVotes
               .FirstOrDefault(x => x.BookId == bookViewModel.BookId
                               && x.UserId == userId);

            if (bookVote == null)
            {
                var book = this.db.Books.FirstOrDefault(x => x.Id == bookViewModel.BookId);

                book.Votes++;

                this.db.BookVotes.Add(new BookVotes
                {
                    UserId = userId,
                    BookId = bookViewModel.BookId,
                    IsVoting = true,
                });
                this.db.SaveChanges();
            }
        }

        public void AddToReadedBooks(ReadBookViewModel value, string userId)
        {
            var book = this.db.ReadedBooks
                .FirstOrDefault(e => e.UserId == userId && e.BookId == value.BookId);

            if (book == null)
            {
                this.db.ReadedBooks.Add(new ReadedBook
                {
                    BookId = value.BookId,
                    UserId = userId,
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
                    BookId = e.BookId,
                }).FirstOrDefault();

            return book;
        }
    }
}
