namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UniBook.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int> CreateAsync(PostViewModel postInputModel, string userId);

        Task AddCommentAsync(AddCommentViewModel inputModel, string userId);

        Task DeleteCommentAsync(int postId, string userId);

        List<DetailsPostViewModel> All();

        DetailsPostViewModel GetById(int id, string loggedUserId);
    }
}
