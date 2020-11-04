namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
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

        public IActionResult GetById(int id)
        {
            var post = this.postsService.GetById(id);
            return this.View(post);
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

            var userId = this.GetUserId();
            this.postsService.Create(inputModel, userId);
            return this.Redirect("/Posts/All");
        }

        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
