namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Web.ViewModels.Friends;

    public class ProfileService : IFriendService
    {
        private readonly ApplicationDbContext db;

        public ProfileService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ProfileDetailsViewModel All(string userId)
        {
            List<ListUsersViewModel> friends = null;

            if (this.db.UserFriends.Any(e => e.ReceiverId == userId))
            {
                friends = this.db.UserFriends
                .Where(e => !e.IsDeleted && e.ReceiverId == userId)
                .Select(e => new ListUsersViewModel
                {
                    Username = e.Sender.UserName,
                }).ToList();
            }
            else
            {
                friends = this.db.UserFriends
                .Where(e => !e.IsDeleted && e.SenderId == userId)
                .Select(e => new ListUsersViewModel
                {
                    Username = e.Receiver.UserName,
                }).ToList();
            }

            var recivedRequests = this.db.UserFriendRequests
                .Where(e => e.Status != FriendRequestStatus.Accepted)
                .Select(e => new ReceivedFriendshipRequest
                {
                    Id = e.SenderId,
                    Username = e.Sender.UserName,
                }).ToList();

            var readedBooks = this.db.ReadedBooks
                .Where(x => x.UserId == userId)
                .Select(x => new UserBookViewModel
                {
                    Id = x.BookId,
                    Image = x.Book.ImageUrl,
                }).ToList();

            var favotieBooks = this.db.FavoriteBooks
                .Where(x => x.UserId == userId)
                .Select(x => new UserBookViewModel
                {
                    Id = x.BookId,
                    Image = x.Book.ImageUrl,
                }).ToList();

            var viewModel = new ProfileDetailsViewModel
            {
                Friends = friends,
                RecivedFriendshipRequests = recivedRequests,
                ReadedBooks = readedBooks,
                FavoriteBooks = favotieBooks,
            };

            return viewModel;
        }

        public bool IsAlreadyFriend(string userId, string username)
        {
            var user = this.FindReciver(username);
            return this.db.UserFriends.Any(
                e => (e.SenderId == userId && e.ReceiverId == user.Id) || (e.SenderId == user.Id && e.ReceiverId == userId));
        }

        public bool IsSendRequestFriendship(string userId, string username)
        {
            var user = this.FindReciver(username);
            return this.db.UserFriendRequests
                .Any(e => e.Sender.Id == userId && e.Receiver.Id == user.Id);
        }

        public string SendRequest(string senderId, string username)
        {
            ApplicationUser reciver = this.FindReciver(username);

            if (reciver == null)
            {
                return null;
            }

            this.db.UserFriendRequests.Add(new FriendRequest
            {
                SenderId = senderId,
                Receiver = reciver,
            });

            this.db.SaveChanges();

            return reciver.Id;
        }

        public void Accept(string senderId, string username)
        {
            var reciver = this.FindReciver(username);
            if (this.db.UserFriends.Any(e => e.SenderId == senderId
            && e.ReceiverId == reciver.Id))
            {
                return;
            }

            this.db.UserFriends.Add(new Friend
            {
                SenderId = senderId,
                ReceiverId = reciver.Id,
                CreatedOn = DateTime.UtcNow,
            });

            this.db.SaveChanges();
        }

        public void UpdateStatus(string username, string sender)
        {
            var reciver = this.FindReciver(username);
            var request = this.db.UserFriendRequests
                .FirstOrDefault(e => e.SenderId == reciver.Id && e.ReceiverId == sender);

            if (request != null)
            {
                request.Status = FriendRequestStatus.Accepted;
                this.db.SaveChanges();
            }
        }

        private ApplicationUser FindReciver(string username)
        {
            return this.db.Users.FirstOrDefault(e => e.UserName == username);
        }
    }
}
