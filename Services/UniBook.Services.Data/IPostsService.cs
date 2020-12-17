namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostViewModel postInputModel, string userId);

        Task<int> AddCommentAsync(AddCommentViewModel inputModel, string userId);

        Task<int> DeleteCommentAsync(int postId, string userId);

        List<DetailsPostViewModel> All();

        List<CategoryInputModel> GetCategories();

        DetailsPostViewModel GetById(int id, string loggedUserId);

        int LikePost(int id, string userId);

        int VoteDown(int id, string userId);
    }
}
