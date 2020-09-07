using Microsoft.AspNetCore.Mvc;

namespace UniBook.Web.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
