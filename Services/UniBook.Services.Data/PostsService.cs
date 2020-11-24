namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public List<DetailsPostViewModel> All()
        {
            var posts = this.db.Posts
                .Select(e => new DetailsPostViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Content = e.Content,
                    Author = e.User.Email,
                    Comments = e.PostComments.Select(c => new CommentPostViewModel
                    {
                        Body = c.CommentBody,
                        PostId = c.PostId.ToString(),
                        UserName = c.User.Email,
                    }).ToList(),
                }).ToList();

            return posts;
        }

        public DetailsPostViewModel GetById(int id, string loggedUserId)
        {
            var currentPost = this.db.Posts
                .Where(e => e.Id == id)
                .Select(e => new DetailsPostViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Content = e.Content,
                    Comments = e.PostComments.Select(x => new CommentPostViewModel
                    {
                        UserName = x.User.UserName,
                        Body = x.CommentBody,
                        PostId = x.PostId.ToString(),
                        LoggedUserId = loggedUserId,
                    }).ToList(),
                    Author = e.User.UserName,
                }).FirstOrDefault();

            return currentPost;
        }

        public async Task<int> CreateAsync(PostViewModel postInputModel, string userId)
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

            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;
        }

        public async Task AddCommentAsync(AddCommentViewModel inputModel, string userId)
        {
            await this.db.PostComments.AddAsync(new PostComment
            {
                PostId = inputModel.PostId,
                UserId = userId,
                CommentBody = inputModel.Body,
            });

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int postId, string userId)
        {
            var comment = this.db.PostComments.Where(e => e.PostId == postId && e.UserId == userId).FirstOrDefault();

            this.db.PostComments.Remove(comment);
            await this.db.SaveChangesAsync();
        }
    }
}
