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
                Title = e.Title,
                Content = e.Content,
                Category = e.Category.Name,
            }).ToList();

            return posts;
        }

        public void Create(PostViewModel postInputModel)
        {
            var category = this.db.Categories.FirstOrDefault(e => e.Name == postInputModel.Category);

            var post = new Post
            {
                Title = postInputModel.Title,
                Category = category,
                Content = postInputModel.Content,
                CreatedOn = DateTime.UtcNow,
            };

            this.db.Posts.Add(post);
            this.db.SaveChanges();
        }
    }
}
