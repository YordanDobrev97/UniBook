namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels;

    public interface IBookService
    {
        IEnumerable<ListAllBooksViewModel> All();

        ContentBookViewModel ReadBook(int id, string userId);

        DetailsBookViewModel Details(int id);
    }
}
