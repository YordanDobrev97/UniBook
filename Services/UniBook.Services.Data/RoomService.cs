namespace UniBook.Services.Data
{
    using System.Linq;

    using UniBook.Data;
    using UniBook.Data.Models;

    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext db;

        public RoomService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddMessageRoom(int messageId, int roomId, string userId)
        {
            this.db.MessagesRoom.Add(new MessageRoom
            {
                MessageId = messageId,
                RoomId = roomId,
                UserId = userId,
            });

            this.db.SaveChanges();
        }

        public Room Create(string name)
        {
            var room = new Room
            {
                Name = name,
            };

            this.db.Rooms.Add(room);
            this.db.SaveChanges();

            return room;
        }

        public Room IsExistRoom(string name)
        {
            return this.db.Rooms.FirstOrDefault(x => x.Name == name);
        }
    }
}
