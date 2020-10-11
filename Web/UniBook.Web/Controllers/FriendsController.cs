namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FriendsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
