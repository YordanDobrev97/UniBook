namespace UniBook.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public void TestSaveBookPageWithInvalidBookId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(user);
            db.SaveChanges();

            var usersService = new UsersService(db);
            var result = usersService
                .SaveBookPage(
                    new ReadBookViewModel
            {
                BookId = 0,
            }, user.Id);

            Assert.False(result);
        }

        [Fact]
        public void TestSaveBookPageWithInvalidUserId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "Book name",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            db.Books.Add(book);
            db.UserReadBooks.Add(new UserReadBook
            {
                Book = book,
                User = new ApplicationUser
                {
                    UserName = "test",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            });

            db.SaveChanges();

            var usersService = new UsersService(db);
            var result = usersService
                .SaveBookPage(
                    new ReadBookViewModel
                    {
                        BookId = book.Id,
                    }, "invalid");

            Assert.False(result);
        }

        [Fact]
        public void TestSaveBookPageSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "Book name",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            var user = new ApplicationUser
            {
                UserName = "test",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.UserReadBooks.Add(new UserReadBook
            {
                Book = book,
                User = user,
            });

            db.SaveChanges();

            var usersService = new UsersService(db);
            var result = usersService
                .SaveBookPage(
                    new ReadBookViewModel
                    {
                        BookId = book.Id,
                    }, user.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task VoteBookTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "something name",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "something user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await db.Books.AddAsync(book);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            var usersService = new UsersService(db);
            usersService.VoteBook(
                new VoteBookViewModel
            {
                BookId = book.Id,
            }, user.Id);

            var votingCount = await db.BookVotes.CountAsync();
            Assert.Equal(1, votingCount);
        }

        [Fact]
        public void TryAddToReadedBooksWithInvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var usersService = new UsersService(db);
            var result = usersService.AddToReadedBooks(0, "not exist user");
            Assert.False(result);
        }

        [Fact]
        public void TryAddAlreadyAddedReadedBooks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "name book 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.Users.Add(user);
            db.ReadedBooks.Add(new ReadedBook
            {
                Book = book,
                User = user,
            });
            db.SaveChanges();
            var service = new UsersService(db);
            var result = service.AddToReadedBooks(book.Id, user.Id);
            Assert.False(result);
        }

        [Fact]
        public void AddReadedBooksSuccesfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "name book 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.Users.Add(user);
            db.SaveChanges();
            var service = new UsersService(db);
            var result = service.AddToReadedBooks(book.Id, user.Id);
            Assert.True(result);
        }

        [Fact]
        public void TryAddToFavoriteBooksWithInvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var usersService = new UsersService(db);
            var result = usersService.AddToFavoriteBooks(0, "not exist user");
            Assert.False(result);
        }

        [Fact]
        public void TryAddAlreadyAddedFavoriteBooks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "name book 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.Users.Add(user);
            db.FavoriteBooks.Add(new FavoriteBook
            {
                Book = book,
                User = user,
            });
            db.SaveChanges();
            var service = new UsersService(db);
            var result = service.AddToFavoriteBooks(book.Id, user.Id);
            Assert.False(result);
        }

        [Fact]
        public void AddToFavoriteBooksSuccesfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "name book 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.Users.Add(user);
            db.SaveChanges();
            var service = new UsersService(db);
            var result = service.AddToFavoriteBooks(book.Id, user.Id);
            Assert.True(result);
        }
    }
}
