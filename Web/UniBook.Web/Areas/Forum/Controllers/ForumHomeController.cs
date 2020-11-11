namespace UniBook.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;

    public class ForumHomeController : ForumBaseController
    {
        private readonly IPostsService postsService;

        public ForumHomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult ForumIndex()
        {
            var posts = this.postsService.All();
            return this.View(posts);
        }
    }
}
