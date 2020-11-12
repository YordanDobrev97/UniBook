namespace UniBook.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using UniBook.Common.Extensions;
    using UniBook.Web.ViewModels.Books;

    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(BooksListViewModel result)
        {
            return this.View(result);
        }
    }
}
