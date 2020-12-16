namespace UniBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;

    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext db;

        public MessageService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int Create(string message)
        {
            var newMessage = new Message
            {
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                TextMessage = message,
            };

            this.db.Messages.Add(newMessage);

            this.db.SaveChanges();
            return newMessage.Id;
        }

        public List<string> GetMessages(string user, int room)
        {
            var messages = this.db.MessagesRoom
                .Where(x => x.RoomId == room)
                .GroupBy(x => new { x.MessageId, x.Message.TextMessage })
                .Select(x => x.Key.TextMessage).ToList();
            return messages;
        }
    }
}
