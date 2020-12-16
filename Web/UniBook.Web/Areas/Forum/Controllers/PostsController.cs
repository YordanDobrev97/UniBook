namespace UniBook.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Posts;

    public class PostsController : ForumBaseController
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult GetById(int id)
        {
            var userId = this.GetUserId();
            var post = this.postsService.GetById(id, userId);
            return this.View(post);
        }

        public IActionResult Create()
        {
            var categories = this.postsService.GetCategories();
            var viewModel = new PostViewModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.GetUserId();
            int postId = await this.postsService.CreateAsync(inputModel, userId);
            return this.RedirectToAction("GetById", "Posts", new { id = postId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel commentViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(commentViewModel);
            }

            var userId = this.GetUserId();
            await this.postsService.AddCommentAsync(commentViewModel, userId);
            return this.RedirectToAction("GetById", "Posts", new { id = commentViewModel.PostId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int postId, string userId)
        {
            await this.postsService.DeleteCommentAsync(postId, userId);
            return this.RedirectToAction("GetById", "Posts", new { id = postId });
        }

        [Authorize]
        [Route("LikePost/{postId}")]
        [HttpPost]
        public IActionResult LikePost(VotePostViewModel viewModel)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int countLikes = this.postsService.LikePost(viewModel.Id, userId);
            return this.Ok(countLikes);
        }

        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
