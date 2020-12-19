namespace UniBook.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using UniBook.Data;
    using UniBook.Data.Models;
    using Xunit;

    public class MessageServiceTests
    {
        [Fact]
        public void CreateMessageSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UniBookDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new MessageService(dbContext);
            var id = service.Create("create message");
            Assert.Equal(1, id);
        }

        [Fact]
        public void GetMessagesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("UniBookDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var user = new ApplicationUser
            {
                UserName = "pesho",
            };

            var room = new Room
            {
                Name = "new room",
            };

            dbContext.MessagesRoom.Add(new MessageRoom
            {
                Message = new Message
                {
                    TextMessage = "test message",
                },
                Room = room,
                User = user,
            });

            dbContext.MessagesRoom.Add(new MessageRoom
            {
                Message = new Message
                {
                    TextMessage = "test message",
                },
                Room = room,
                User = user,
            });
            dbContext.SaveChanges();

            var service = new MessageService(dbContext);
            int countMessages = service.GetMessages(user.Id, room.Id).Count;
            Assert.Equal(2, countMessages);
        }
    }
}
