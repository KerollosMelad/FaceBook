using System;
using System.Collections.Generic;
using System.Text;
using FaceBook_InitialVersion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaceBook_InitialVersion.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        #region Tables
        public virtual DbSet<Person> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserPostLike> UserPostLikes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<UserPostComment> UserPostComments { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; } 
        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region userPostComment Relations
            builder.Entity<UserPostComment>()
                    .HasKey(K => new { K.userID, K.PostID, K.CommentID });

            builder.Entity<UserPostLike>()
             .HasKey(K => new { K.UserID, K.PostID });

            #endregion

            #region Friendship Relations
            builder.Entity<Friendship>()
               .HasKey(K => new { K._userID, K._friendID });

            builder.Entity<Friendship>()
                .HasOne(US => US.User)
                .WithMany(F => F.FriendRequestMade)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(US => US.Friend)
                .WithMany(F => F.FriendRequestAccepted)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Friendship>()
            //    .HasKey(FS => FS.ID);

            //builder.Entity<Person>()
            //    .HasMany(FS => FS.friendships)
            //    .WithOne(M => M.User);


            #endregion
        }

    }
}
