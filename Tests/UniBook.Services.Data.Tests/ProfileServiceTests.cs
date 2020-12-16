namespace UniBook.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using UniBook.Services.Data;
    using Xunit;

    public class ProfileServiceTests
    {
        [Fact]
        public void SendFriendshipRequestCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var senderUser = new ApplicationUser
            {
                UserName = "sender user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var reciverUser = new ApplicationUser
            {
                UserName = "reciver user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            dbContext.Users.Add(senderUser);
            dbContext.Users.Add(reciverUser);

            dbContext.SaveChanges();

            var profileService = new ProfileService(dbContext);
            string result = profileService.SendRequest(senderUser.Id, reciverUser.UserName);
            Assert.Equal(reciverUser.Id, result);
        }

        [Fact]
        public void SendFriendshipRequestShouldBeReturnNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var senderUser = new ApplicationUser
            {
                UserName = "sender user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var reciverUser = new ApplicationUser
            {
                UserName = "reciver user",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            dbContext.Users.Add(senderUser);
            dbContext.SaveChanges();

            var profileService = new ProfileService(dbContext);
            string result = profileService.SendRequest(senderUser.Id, reciverUser.UserName);
            Assert.Null(result);
        }

        [Fact]
        public void AcceptFriendshipTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDb").Options;
            var db = new ApplicationDbContext(options);

            var firstUser = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            var secondUser = new ApplicationUser
            {
                UserName = "user 2",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(firstUser);
            db.Users.Add(secondUser);
            db.SaveChanges();

            var profileService = new ProfileService(db);
            profileService.Accept(firstUser.Id, secondUser.UserName);
            var countUserFriends = db.UserFriends.Count();
            Assert.Equal(1, countUserFriends);
        }

        [Fact]
        public void IsAlreadyFriendTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDatabase").Options;
            var db = new ApplicationDbContext(options);

            var firstUser = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            var secondUser = new ApplicationUser
            {
                UserName = "user 2",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            db.Users.Add(firstUser);
            db.Users.Add(secondUser);

            db.UserFriends.Add(new Friend
            {
                Sender = firstUser,
                Receiver = secondUser,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            db.SaveChanges();

            var profileService = new ProfileService(db);
            var result = profileService.IsAlreadyFriend(firstUser.Id, secondUser.UserName);
            Assert.True(result);
        }

        [Fact]
        public void IsSendRequestFriendshipTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbDemo").Options;
            var db = new ApplicationDbContext(options);

            var firstUser = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var secondUser = new ApplicationUser
            {
                UserName = "user 2",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var thirdUser = new ApplicationUser
            {
                UserName = "user 3",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(firstUser);
            db.Users.Add(secondUser);
            db.Users.Add(thirdUser);

            db.UserFriendRequests.Add(new FriendRequest
            {
                Sender = firstUser,
                Receiver = secondUser,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            });

            db.SaveChanges();

            var profileService = new ProfileService(db);
            var firstSendRequest = profileService.IsSendRequestFriendship(firstUser.Id, secondUser.UserName);
            var secondSendRequest = profileService.IsSendRequestFriendship(firstUser.Id, thirdUser.UserName);

            Assert.True(firstSendRequest);
            Assert.False(secondSendRequest);
        }

        [Fact]
        public void UpdateStatusTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbStatus").Options;
            var db = new ApplicationDbContext(options);

            var firstUser = new ApplicationUser
            {
                UserName = "user 1",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var secondUser = new ApplicationUser
            {
                UserName = "user 2",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var friendship = new FriendRequest
            {
                Sender = firstUser,
                Receiver = secondUser,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            db.Users.Add(firstUser);
            db.Users.Add(secondUser);
            db.UserFriendRequests.Add(friendship);
            db.SaveChanges();

            var profileService = new ProfileService(db);
            profileService.UpdateStatus(firstUser.UserName, secondUser.Id);
            Assert.Equal(FriendRequestStatus.Accepted, friendship.Status);
        }
    }
}
