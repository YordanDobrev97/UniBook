using UniBook.Models;

namespace UniBook.Services.Interfaces
{
    public interface IBookService
    {
        Book FindById(int id);

        Book FindByName(string title);

        Book FindByAuthor(string author);
    }
}
