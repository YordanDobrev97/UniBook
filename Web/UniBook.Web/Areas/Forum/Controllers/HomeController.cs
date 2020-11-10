namespace UniBook.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;

    public class HomeController : ForumBaseController
    {
        private readonly IPostsService postsService;

        public HomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            var posts = this.postsService.All();
            return this.View(posts);
        }
    }
}
