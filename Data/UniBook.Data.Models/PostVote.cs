﻿namespace UniBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostVote
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CountPositive { get; set; }

        public int CountNegative { get; set; }
    }
}
