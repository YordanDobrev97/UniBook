namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult All()
        {
            var posts = this.postsService.All();
            return this.View(posts);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(PostViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.postsService.Create(inputModel);
            return this.Redirect("/Posts/All");
        }
    }
}
