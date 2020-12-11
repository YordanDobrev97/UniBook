namespace UniBook.Data.Models
{
    using UniBook.Data.Common.Models;

    public class MessageRoom : BaseDeletableModel<int>
    {
        public int MessageId { get; set; }

        public Message Message { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
