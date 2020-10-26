namespace UniBook.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data.Common.Repositories;
    using UniBook.Data.Models;

    public class UserBookService : IUserBookService
    {
        private readonly IDeletableEntityRepository<UserBook> repository;

        public UserBookService(IDeletableEntityRepository<UserBook> repository)
        {
            this.repository = repository;
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
    }
}
