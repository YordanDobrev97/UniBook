namespace UniBook.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using Xunit;

    public class BookServiceTests
    {
        [Fact]
        public void TestSortByAlphabeticalCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Add(new Book
            {
                Name = "Хари Потър",
            });

            dbContext.Add(new Book
            {
                Name = "Алената буква",
            });
            dbContext.SaveChanges();

            var bookService = new BookService(dbContext);
            var firstBook = bookService.SortByAlphabetical().FirstOrDefault();

            Assert.Equal("Алената буква", firstBook.Name);
        }

        [Fact]
        public void TestSortByLikesCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Add(new Book
            {
                Name = "Алената буква",
                Votes = 50,
            });

            dbContext.Add(new Book
            {
                Name = "Хари Потър",
                Votes = 200,
            });

            dbContext.SaveChanges();

            var bookService = new BookService(dbContext);
            var firstBook = bookService.SortByLikes().FirstOrDefault();

            Assert.Equal("Хари Потър", firstBook.Name);
        }

        [Fact]
        public void TestSortLatestAddedCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Add(new Book
            {
                Name = "Аз преди теб",
                Votes = 2,
            });

            dbContext.Add(new Book
            {
                Name = "Под игото",
                Votes = 50,
            });

            dbContext.SaveChanges();

            var bookService = new BookService(dbContext);
            var firstBook = bookService.SortLatestAdded().FirstOrDefault();

            Assert.Equal("Под игото", firstBook.Name);
        }

        [Fact]
        public void TestGetAuthorBooksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstBook = new Book { Name = "Под игото" };
            var secondBook = new Book { Name = "Нора" };

            var author = new Author { Name = "Иван Вазов" };
            author.Books.Add(firstBook);
            author.Books.Add(secondBook);

            dbContext.Authors.Add(author);

            dbContext.SaveChanges();

            var bookService = new BookService(dbContext);
            var authorBooks = bookService.GetAuthorBooks(author.Name);

            Assert.Equal(2, authorBooks.Count());
        }

        [Theory]
        [InlineData("Хари Потър")]
        [InlineData("Под игото")]
        [InlineData("Алената буква")]
        [InlineData("Човек на име уве")]
        public void TestSearchBookByName(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            db.Books.Add(new Book
            {
                Name = name,
                Author = new Author
                {
                    Name = "Test author",
                },
            });

            db.SaveChanges();
            var bookService = new BookService(db);
            var book = bookService.SearchByBook(name).FirstOrDefault();
            Assert.Equal(name, book.Name);
        }

        [Theory]
        [InlineData(2012)]
        [InlineData(2020)]
        public void TestSearchByYear(int year)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            db.Books.Add(new Book
            {
                Name = "Test",
                Author = new Author { Name = "Test Author" },
                YearIssued = new YearIssued
                {
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                    YearOfIssue = year,
                },
            });

            db.SaveChanges();
            var bookService = new BookService(db);
            var yearBooks = bookService.SearchByYear(year);

            Assert.Single(yearBooks);
        }

        [Theory]
        [InlineData("Роман", "Под игото")]
        [InlineData("Приключения", "Бялото мълчание")]
        [InlineData("Фантастика", "Гори, вещице, гори")]

        public void TestSeachByGenre(string genre, string bookName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            db.Books.Add(new Book
            {
                Name = bookName,
                Author = new Author { Name = "Test Author" },
                Genre = new Genre
                {
                    Name = genre,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            });

            db.SaveChanges();
            var bookService = new BookService(db);
            var genreBook = bookService.SearchByGenres(new string[] { genre }).FirstOrDefault();

            Assert.Equal(bookName, genreBook.Name);
        }

        [Fact]
        public void TestSearchFreeBooks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            db.Books.Add(new Book
            {
                Name = "Test Book Name 1",
                Author = new Author
                {
                    Name = "Test",
                },
                IsFree = true,
            });

            db.Books.Add(new Book
            {
                Name = "Test Book Name 2",
                Author = new Author
                {
                    Name = "Test",
                },
                IsFree = true,
            });

            db.SaveChanges();

            var bookSerivce = new BookService(db);
            var paidBooks = bookSerivce.SearchFreeBooks();
            Assert.Equal(2, paidBooks.Count());
        }

        [Fact]
        public void TestAddCommentToBook()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "Test user",
            };

            var book = new Book
            {
                Name = "Test Book Name 1",
                Author = new Author
                {
                    Name = "Test",
                },
                IsFree = true,
            };

            db.Books.Add(book);
            db.Users.Add(user);

            db.SaveChanges();

            var bookSerivce = new BookService(db);
            bookSerivce.AddComment(user.Id, book.Id, "Test comment");

            var bookComment = db.BookComments
                .Where(x => x.BookId == book.Id && x.UserId == user.Id)
                .FirstOrDefault();

            Assert.Equal("Test comment", bookComment.CommentBody);
        }

        [Fact]
        public void TestGetReadedBooks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "Test user",
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
            };
            var book = new Book
            {
                Name = "test book name",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(user);
            db.Books.Add(book);

            db.ReadedBooks.Add(new ReadedBook
            {
                User = user,
                Book = book,
            });

            db.SaveChanges();
            var bookService = new BookService(db);
            var readedBooks = bookService.GetReadedBooks(user.Id);
            Assert.Single(readedBooks);
        }

        [Fact]
        public void TestReadBook()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "Demo user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var book = new Book
            {
                Name = "book name",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(user);
            db.Books.Add(book);

            db.SaveChanges();

            var bookService = new BookService(db);
            var readUserBook = bookService.ReadBook(book.Id, user.Id);
            var invalidUserBook = bookService.ReadBook(-1, "Invalid user id");

            Assert.NotNull(readUserBook);
            Assert.Null(invalidUserBook);
        }

        [Fact]
        public void TestGetPaymentDetails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTest").Options;
            var db = new ApplicationDbContext(options);

            var book = new Book
            {
                Name = "The best book",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "payment user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Books.Add(book);
            db.Users.Add(user);
            db.SaveChanges();

            var bookService = new BookService(db);
            var paymentDetails = bookService.PaymentDetails(book.Id, user.Id);
            var invalidPayment = bookService.PaymentDetails(0, "not exist user");
            Assert.NotNull(paymentDetails);
            Assert.Null(invalidPayment);
        }

        [Theory]
        [InlineData("Роман", "Фантастика", "История", "Хорър", "Приказка")]
        public void TestGetAllGenres(params string[] names)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbTestGenres").Options;
            var db = new ApplicationDbContext(options);

            foreach (var item in names)
            {
                db.Genres.Add(new Genre
                {
                    Name = item,
                    CreatedOn= DateTime.UtcNow,
                    IsDeleted = false,
                });
            }

            db.SaveChanges();
            var genres = new BookService(db);
            var countGenres = genres.GetGenres().Count();

            Assert.Equal(names.Length, countGenres);
        }

        [Theory]
        [InlineData(2020, 2019, 2018)]
        public void TestGetYears(params int[] years)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbTestYears").Options;
            var db = new ApplicationDbContext(options);

            foreach (int item in years)
            {
                db.YearIssueds.Add(new YearIssued
                {
                    YearOfIssue = item,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                });
            }

            db.SaveChanges();
            var bookService = new BookService(db);
            var countYears = bookService.GetYears().Count();

            Assert.Equal(years.Length, countYears);
        }
    }
}
