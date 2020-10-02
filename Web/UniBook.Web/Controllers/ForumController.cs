namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
