namespace UniBook.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Web.ViewModels;

    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel model)
        {
            return this.View(model);
        }
    }
}
