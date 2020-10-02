namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
