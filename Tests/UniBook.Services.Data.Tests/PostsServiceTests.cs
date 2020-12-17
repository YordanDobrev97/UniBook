namespace UniBook.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Posts;
    using Xunit;

    public class PostsServiceTests
    {
        [Theory]
        [InlineData("Новини")]
        [InlineData("Предложения и проблеми")]
        [InlineData("Книги")]
        public void CreateAsyncTest(string category)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbPosts").Options;
            var dbContext = new ApplicationDbContext(options);

            var categories = new List<CategoryInputModel>();
            categories.Add(new CategoryInputModel
            {
                Name = category,
            });
            var user = new ApplicationUser
            {
                UserName = "new user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var viewModel = new PostViewModel
            {
                Title = "new post",
                Categories = categories,
                Content = "content post...",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var postService = new PostsService(dbContext);
            var postId = postService.CreateAsync(viewModel, user.Id);
            Assert.True(postId.IsCompleted);
        }

        [Theory]
        [InlineData("comment body 1", 1)]
        [InlineData("comment body 1", -1)]
        public async Task AddCommentAsyncTest(string commentBody, int postId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("UniBookDbPosts").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "test user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var post = new Post
            {
                Title = "new post",
                Content = "something...",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await db.Users.AddAsync(user);
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            var postService = new PostsService(db);
            var resultPostId = await postService.AddCommentAsync(
                new AddCommentViewModel
                {
                    PostId = postId,
                    Body = commentBody,
                }, user.Id);

            Assert.Equal(postId, resultPostId);
            Assert.NotEqual(0, resultPostId);
        }

        [Fact]
        public async Task DeleteCommentAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("UniBookDbComments").Options;
            var db = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "test user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var post = new Post
            {
                Title = "new post",
                Content = "something...",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            await db.Users.AddAsync(user);
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            var postService = new PostsService(db);
            await postService.AddCommentAsync(
                new AddCommentViewModel
                {
                    PostId = post.Id,
                    Body = "add comment",
                }, user.Id);

            var result = await postService.DeleteCommentAsync(post.Id, user.Id);
            Assert.Equal(post.Id, result);
        }

        [Fact]
        public void VoteUpPostTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "Like post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something like...",
                Category = new Category
                {
                    Name = "like post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "like post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.LikePost(post.Id, user.Id);
            Assert.Equal(1, result);
        }

        [Fact]
        public void ChageVoteUpToDownPostTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "Like post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something like...",
                Category = new Category
                {
                    Name = "like post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "like post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.PostVotes.Add(new PostVote
            {
                Post = post,
                User = user,
                TypeVote = VoteType.Up,
            });
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.LikePost(post.Id, user.Id);
            Assert.Equal(0, result);
        }

        [Fact]
        public void ChageVoteDownToUpPostTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "Like post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something like...",
                Category = new Category
                {
                    Name = "like post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "like post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.PostVotes.Add(new PostVote
            {
                Post = post,
                User = user,
                TypeVote = VoteType.Down,
            });
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.LikePost(post.Id, user.Id);
            Assert.Equal(1, result);
        }

        [Fact]
        public void VoteDownTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "down post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something down...",
                Category = new Category
                {
                    Name = "down post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "down post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.VoteDown(post.Id, user.Id);
            Assert.Equal(1, result);
        }

        [Fact]
        public void ChageDownVoteDownToUpPostTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "down post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something down...",
                Category = new Category
                {
                    Name = "down post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "down post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.PostVotes.Add(new PostVote
            {
                Post = post,
                User = user,
                TypeVote = VoteType.Down,
            });
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.VoteDown(post.Id, user.Id);
            Assert.Equal(0, result);
        }

        [Fact]
        public void ChageDownVoteUpToDownPostTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var post = new Post
            {
                Title = "down post",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Content = "something down...",
                Category = new Category
                {
                    Name = "down post category",
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                },
            };

            var user = new ApplicationUser
            {
                UserName = "down post user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Posts.Add(post);
            db.Users.Add(user);
            db.PostVotes.Add(new PostVote
            {
                Post = post,
                User = user,
                TypeVote = VoteType.Up,
            });
            db.SaveChanges();

            var postService = new PostsService(db);
            var result = postService.VoteDown(post.Id, user.Id);
            Assert.Equal(1, result);
        }

        [Fact]
        public void TryVoteUpNotExistPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var postService = new PostsService(db);
            var result = postService.LikePost(3, "not exist user");
            Assert.Equal(0, result);
        }

        [Fact]
        public void TryVoteDownNotExistPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UniBookDbVotes").Options;
            var db = new ApplicationDbContext(options);

            var postService = new PostsService(db);
            var result = postService.VoteDown(12, "not exist user");
            Assert.Equal(0, result);
        }
    }
}
