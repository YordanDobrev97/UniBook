using Microsoft.AspNetCore.Mvc;

namespace UniBook.Web.Controllers
{
    public class ForumController : Controller
    {

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
