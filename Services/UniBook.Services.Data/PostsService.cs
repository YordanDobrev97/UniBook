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
            var countPositiveVotes = this.db
                .PostVotes
                .Where(x => x.PostId == id && x.TypeVote == VoteType.Up)
                .Count();

            var countNegativeVotes = this.db
                .PostVotes
                .Where(x => x.PostId == id && x.TypeVote == VoteType.Down)
                .Count();

            var currentPost = this.db.Posts
                .Where(e => e.Id == id)
                .Select(e => new DetailsPostViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Content = e.Content,
                    CountPositiveComments = countPositiveVotes,
                    CountNegativeComments = countNegativeVotes,
                    Comments = e.PostComments.Select(x => new CommentPostViewModel
                    {
                        UserName = x.User.UserName,
                        Body = x.CommentBody,
                        PostId = x.UserId,
                        LoggedUserId = loggedUserId,
                    }).ToList(),
                    Author = e.User.UserName,
                }).FirstOrDefault();

            return currentPost;
        }

        public async Task<int> CreateAsync(PostViewModel postInputModel, string userId)
        {
            var post = new Post
            {
                Title = postInputModel.Title,
                CategoryId = postInputModel.CategoryId,
                Content = postInputModel.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
            };

            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;
        }

        public async Task<int> AddCommentAsync(AddCommentViewModel inputModel, string userId)
        {
            if (!this.db.Posts.Any(x => x.Id == inputModel.PostId)
                || !this.db.Users.Any(x => x.Id == userId))
            {
                return -1;
            }

            var postComment = new PostComment
            {
                PostId = inputModel.PostId,
                UserId = userId,
                CommentBody = inputModel.Body,
            };

            await this.db.PostComments.AddAsync(postComment);
            await this.db.SaveChangesAsync();

            return postComment.PostId;
        }

        public async Task<int> DeleteCommentAsync(int postId, string userId)
        {
            if (!this.db.PostComments.Any(x => x.PostId == postId)
                || !this.db.PostComments.Any(x => x.UserId == userId))
            {
                return 0;
            }

            var comment = this.db.PostComments
                .Where(e => e.PostId == postId && e.UserId == userId).FirstOrDefault();

            this.db.PostComments.Remove(comment);
            await this.db.SaveChangesAsync();

            return comment.PostId;
        }

        public List<CategoryInputModel> GetCategories()
        {
            var categories = this.db.Categories
                .Select(c => new CategoryInputModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

            return categories;
        }

        public int LikePost(int id, string userId)
        {
            if (!this.ExistPost(id))
            {
                return 0;
            }

            var existPost = this.db.PostVotes
                .FirstOrDefault(x => x.PostId == id && x.UserId == userId);

            if (existPost == null)
            {
                existPost = new PostVote
                {
                    PostId = id,
                    UserId = userId,
                    TypeVote = VoteType.Up,
                };

                this.db.PostVotes.Add(existPost);
            }
            else if (existPost.TypeVote == VoteType.Up)
            {
                existPost.TypeVote = VoteType.Down;
            }
            else if (existPost.TypeVote == VoteType.Down)
            {
                existPost.TypeVote = VoteType.Up;
            }

            this.db.SaveChanges();

            int count = this.db.PostVotes
                .Count(x => x.PostId == id && x.TypeVote == VoteType.Up);
            return count;
        }

        public int VoteDown(int id, string userId)
        {
            if (!this.ExistPost(id))
            {
                return 0;
            }

            var existPost = this.db.PostVotes.FirstOrDefault(x => x.PostId == id && x.UserId == userId);

            if (existPost == null)
            {
                existPost = new PostVote
                {
                    PostId = id,
                    UserId = userId,
                    TypeVote = VoteType.Down,
                };

                this.db.PostVotes.Add(existPost);
            }
            else if (existPost.TypeVote == VoteType.Down)
            {
                existPost.TypeVote = VoteType.Up;
            }
            else if (existPost.TypeVote == VoteType.Up)
            {
                existPost.TypeVote = VoteType.Down;
            }

            this.db.SaveChanges();
            return this.db.PostVotes
                .Count(x => x.PostId == id && x.TypeVote == VoteType.Down);
        }

        private bool ExistPost(int id)
        {
            return this.db.Posts.Any(x => x.Id == id);
        }
    }
}
