namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        public IActionResult Details()
        {
            return this.View();
        }
    }
}
