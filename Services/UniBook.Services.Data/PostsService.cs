namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;

    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext db;

        public PostsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<PostViewModel> All()
        {
            var posts = this.db.Posts
                .Select(e => new PostViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Content = e.Content,
                    Category = e.Category.Name,
                }).ToList();

            return posts;
        }

        public void Create(PostViewModel postInputModel, string userId)
        {
            var category = this.db.Categories.FirstOrDefault(e => e.Name == postInputModel.Category);

            var post = new Post
            {
                Title = postInputModel.Title,
                Category = category,
                Content = postInputModel.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
            };

            this.db.Posts.Add(post);
            this.db.SaveChanges();
        }

        public DetailsPostViewModel GetById(int id)
        {
            var currentPost = this.db.Posts
                .Where(e => e.Id == id)
                .Select(e => new DetailsPostViewModel
                {
                    Title = e.Title,
                    Content = e.Content,
                    Comments = e.PostComments.Select(x => new CommentPostViewModel
                    {
                        UserName = x.User.UserName,
                        CommentBody = x.CommentBody,
                    }).ToList(),
                    Author = e.User.UserName,
                }).FirstOrDefault();

            return currentPost;
        }
    }
}
