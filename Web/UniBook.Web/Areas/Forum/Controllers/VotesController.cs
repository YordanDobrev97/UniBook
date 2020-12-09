namespace UniBook.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UniBook.Services.Data;
    using UniBook.Web.ViewModels.Posts;

    [ApiController]
    [Route("/Forum/[controller]/[action]")]
    public class VotesController : ControllerBase
    {
        private readonly IPostsService postsService;

        public VotesController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpPost]
        public IActionResult LikePost(VotePostViewModel votePost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int countLikes = this.postsService.LikePost(votePost.Id, userId);
            return this.Ok(countLikes);
        }

        [HttpPost]
        public IActionResult DownPost(VotePostViewModel votePost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int countLikes = this.postsService.VoteDown(votePost.Id, userId);
            return this.Ok(countLikes);
        }
    }
}
