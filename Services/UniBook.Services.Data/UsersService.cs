﻿namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels;
    using UniBook.Web.ViewModels.Friends;

    public class UsersService : IUsersService
    {
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

            if (!this.db.Books.Any(x => x.Id == bookId)
                || !this.db.Users.Any(x => x.Id == userId))
            {
                return false;
            }

            var userBook = this.db.UserReadBooks
                .FirstOrDefault(e => e.UserId == userId && e.BookId == bookId);

            if (userBook == null)
            {
                this.db.UserReadBooks.Add(new UserReadBook
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

        public bool CheckIsRecivedFriendRequest(string id)
        {
            return this.db.UserFriendRequests
                .Where(e => e.Status != FriendRequestStatus.Accepted)
                .Any(e => e.Receiver.Id == id);
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

        public bool AddToReadedBooks(int bookId, string userId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var book = this.db.Books.FirstOrDefault(x => x.Id == bookId);

            if (user == null || book == null)
            {
                return false;
            }

            if (this.db.ReadedBooks.Any(x => x.BookId == bookId && x.UserId == userId))
            {
                return false;
            }

            this.db.ReadedBooks.Add(new ReadedBook
            {
                Book = book,
                User = user,
            });

            this.db.SaveChanges();
            return true;
        }

        public bool AddToFavoriteBooks(int bookId, string userId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var book = this.db.Books.FirstOrDefault(x => x.Id == bookId);

            if (user == null || book == null)
            {
                return false;
            }

            if (this.db.FavoriteBooks.Any(x => x.BookId == bookId && x.UserId == userId))
            {
                return false;
            }

            this.db.FavoriteBooks.Add(new FavoriteBook
            {
                Book = book,
                User = user,
            });

            this.db.SaveChanges();
            return true;
        }

        public bool IsStartReadBook(string userId, int bookId)
        {
            return this.db.UserReadBooks.Any(e => e.UserId == userId && e.BookId == bookId);
        }

        public ContentBookViewModel GetStartReadBook(string userId, int bookId)
        {
            var book = this.db.UserReadBooks
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
