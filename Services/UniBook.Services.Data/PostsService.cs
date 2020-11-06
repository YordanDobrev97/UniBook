﻿namespace UniBook.Services.Data
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
                    CountComments = e.PostComments.Count,
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
                        PostId = x.UserId,
                        LoggedUserId = loggedUserId,
                    }).ToList(),
                    Author = e.User.UserName,
                }).FirstOrDefault();

            return currentPost;
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

        public void AddComment(AddCommentViewModel inputModel, string userId)
        {
            this.db.PostComments.Add(new PostComment
            {
                PostId = inputModel.PostId,
                UserId = userId,
                CommentBody = inputModel.Body,
            });

            this.db.SaveChanges();
        }

        public void DeleteComment(int postId, string userId)
        {
            var comment = this.db.PostComments.Where(e => e.PostId == postId && e.UserId == userId).FirstOrDefault();

            this.db.PostComments.Remove(comment);
            this.db.SaveChanges();
        }
    }
}
