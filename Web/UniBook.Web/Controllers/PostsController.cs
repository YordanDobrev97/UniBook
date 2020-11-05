namespace UniBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
            var userId = this.GetUserId();
            var post = this.postsService.GetById(id, userId);
            return this.View(post);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(AddCommentViewModel commentViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.GetUserId();
            this.postsService.AddComment(commentViewModel, userId);
            return this.RedirectToAction("GetById", "Posts", new { id = commentViewModel.PostId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteComment(int postId, string userId)
        {
            this.postsService.DeleteComment(postId, userId);
            return this.RedirectToAction("GetById", "Posts", new { id = postId });
        }

        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}