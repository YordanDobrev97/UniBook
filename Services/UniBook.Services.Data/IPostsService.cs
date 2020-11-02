namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using UniBook.Web.ViewModels.Posts;

    public interface IPostsService
    {
        void Create(PostViewModel postInputModel);

        List<PostViewModel> All();
    }
}
