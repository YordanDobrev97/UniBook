namespace UniBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using UniBook.Data.Common.Models;

    public class FriendRequest : BaseDeletableModel<int>
    {
        [Required]
        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        public FriendRequestStatus Status { get; set; }
    }
}
