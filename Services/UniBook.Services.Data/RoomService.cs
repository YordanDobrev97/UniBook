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
            if (this.db.MessagesRoom.Any(
                x => x.MessageId == messageId && x.RoomId == roomId
                && x.UserId == userId))
            {
                return;
            }

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
            if (this.db.Rooms.Any(x => x.Name == name))
            {
                return null;
            }

            var room = new Room
            {
                Name = name,
            };

            this.db.Rooms.Add(room);
            this.db.SaveChanges();

            return room;
        }

        public int GetRoom(string userId)
        {
            int roomId = this.db.Rooms.Where(x => x.Name.Contains(userId))
                .Select(x => x.Id)
                .FirstOrDefault();

            return roomId;
        }

        public Room IsExistRoom(string name)
        {
            return this.db.Rooms.FirstOrDefault(x => x.Name.Contains(name));
        }

        public bool IsExistUser(string userId)
        {
            return this.db.Rooms.Any(x => x.Name.Contains(userId));
        }
    }
}
