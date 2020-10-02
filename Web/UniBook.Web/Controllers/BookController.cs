namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BookController : BaseController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
