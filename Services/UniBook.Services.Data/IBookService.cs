namespace UniBook.Services.Data
{
    using System.Collections.Generic;

    using UniBook.Web.ViewModels.Books;
    using UniBook.Web.ViewModels.Payments;

    public interface IBookService
    {
        IEnumerable<ListAllBooksViewModel> All();

        IEnumerable<ListAllBooksViewModel> GetAllFree();

        IEnumerable<ListAllBooksViewModel> Search(SearchBookViewModel search);

        IEnumerable<ReadedBookViewModel> GetReadedBooks(string userId);

        ContentBookViewModel ReadBook(int id, string userId);

        DetailsBookViewModel Details(int id, string userId);

        BookDetailsViewModel PaymentDetails(int id);
    }
}
