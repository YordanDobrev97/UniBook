namespace UniBook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data.Common.Repositories;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels;

    public class UserBookService : IUserBookService
    {
        private readonly IDeletableEntityRepository<UserBook> repository;
        private readonly IDeletableEntityRepository<Book> booRepository;

        public UserBookService(IDeletableEntityRepository<UserBook> repository, IDeletableEntityRepository<Book> booRepository)
        {
            this.repository = repository;
            this.booRepository = booRepository;
        }

        public async Task SaveAsync(int bookId, string userId, int readCount)
        {
            var userBook = new UserBook
            {
                BookId = bookId,
                UserId = userId,
                ReadCount = readCount,
            };

            var currentUserBook = this.repository.All()
                .Where(x => x.BookId == userBook.BookId && x.UserId == userBook.UserId).FirstOrDefault();

            if (currentUserBook == null)
            {
                await this.repository.AddAsync(userBook);
                await this.repository.SaveChangesAsync();
            }
            else
            {
                await this.Edit(userBook, readCount);
            }
        }

        public async Task Edit(UserBook userBook, int readCount)
        {
            userBook.ReadCount = readCount;
            this.repository.Update(userBook);
            await this.repository.SaveChangesAsync();
        }

        public ContentBookViewModel GetStartReadBook(string userId, int bookId)
        {
            var book = this.repository.All()
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .Select(e => new ContentBookViewModel
                {
                    ReadCount = e.ReadCount,
                    Title = e.Book.Name,
                    Content = e.Book.Body,
                    IsStartRead = e.Book.IsStartRead,
                }).FirstOrDefault();

            return book;
        }

        public void SaveStartRead(int bookId)
        {
            var book = this.booRepository.All()
                .Where(x => x.Id == bookId)
                .FirstOrDefault();

            book.IsStartRead = true;
        }
    }
}
