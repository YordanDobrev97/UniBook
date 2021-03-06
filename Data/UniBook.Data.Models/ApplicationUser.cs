﻿// ReSharper disable VirtualMemberCallInConstructor
namespace UniBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using UniBook.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Posts = new HashSet<Post>();
            this.Payments = new HashSet<Payment>();
            this.Friends = new HashSet<Friend>();
            this.FriendRequestSend = new HashSet<FriendRequest>();
            this.FriendRequestReceived = new HashSet<FriendRequest>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Friend> Friends { get; set; }

        public ICollection<FriendRequest> FriendRequestSend { get; set; }

        public ICollection<FriendRequest> FriendRequestReceived { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
