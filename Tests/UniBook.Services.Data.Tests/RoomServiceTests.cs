namespace UniBook.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using Xunit;

    public class RoomServiceTests
    {
        [Fact]
        public void TestCreateRoom()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var roomService = new RoomService(dbContext);
            var room = roomService.Create("new room");
            Assert.NotNull(room);
        }

        [Fact]
        public void TestCreateExistRoom()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Rooms.Add(new Room
            {
                Name = "new room",
            });
            dbContext.SaveChanges();

            var roomService = new RoomService(dbContext);
            var room = roomService.Create("new room");
            Assert.Null(room);
        }

        [Fact]
        public void AddMessageRoomTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var message = new Message
            {
                TextMessage = "some message...",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var room = new Room
            {
                Name = "new room",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            var user = new ApplicationUser
            {
                UserName = "pesho.petrov",
            };

            dbContext.Messages.Add(message);
            dbContext.Rooms.Add(room);
            dbContext.Users.Add(user);

            dbContext.SaveChanges();
            var roomService = new RoomService(dbContext);
            roomService.AddMessageRoom(message.Id, room.Id, user.Id);
            int countMessageRoom = dbContext.MessagesRoom
                .CountAsync()
                .GetAwaiter()
                .GetResult();

            Assert.Equal(1, countMessageRoom);
        }

        [Fact]
        public void TestExistUserInRoom()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("UniBookDbTest").Options;
            var dbContext = new ApplicationDbContext(options);

            var firstUser = new ApplicationUser
            {
                UserName = "first.petrov",
            };
            var secondUser = new ApplicationUser
            {
                UserName = "second.petrov",
            };

            var room = new Room
            {
                Name = firstUser.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };

            dbContext.Rooms.Add(room);
            dbContext.Users.Add(firstUser);
            dbContext.Users.Add(secondUser);
            dbContext.SaveChanges();

            var roomService = new RoomService(dbContext);
            var existUser = roomService.IsExistUser(firstUser.Id);
            var invalidUser = roomService.IsExistUser(secondUser.Id);

            Assert.True(existUser);
            Assert.False(invalidUser);
        }
    }
}
