namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels.Books;

    public interface IBookService
    {
        IEnumerable<ListAllBooksViewModel> All();

        IEnumerable<ListAllBooksViewModel> GetAllFree();

        IEnumerable<ListAllBooksViewModel> Search(SearchBookViewModel search);

        IEnumerable<ReadedBookViewModel> GetReadedBooks(string userId);

        ContentBookViewModel ReadBook(int id, string userId);

        DetailsBookViewModel Details(int id, string userId);
    }
}
