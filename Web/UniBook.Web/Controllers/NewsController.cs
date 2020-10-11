namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
