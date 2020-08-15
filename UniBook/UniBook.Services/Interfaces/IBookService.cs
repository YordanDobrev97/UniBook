using System.Collections.Generic;
using UniBook.Models;

namespace UniBook.Services.Interfaces
{
    public interface IBookService
    {
        Book FindById(int id);

        Book FindByName(string title);

        Book FindByAuthor(string author);

        void UpVote(int bookId, int rating);

        ICollection<Book> Top50LikedBooks();

        ICollection<Book> All();
    }
}
