using System;
using UniBook.Data;
using UniBook.Data.Models;

namespace UniBook.Services.Data
{
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
    }
}
