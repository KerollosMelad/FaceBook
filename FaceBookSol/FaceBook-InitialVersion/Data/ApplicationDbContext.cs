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


        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserPostLike> UserPostLikes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<UserPostComment> UserPostComments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserPostComment>()
                .HasKey(K => new { K.userID, K.PostID, K.CommentID });

            builder.Entity<UserPostLike>()
             .HasKey(K => new { K.UserID, K.PostID});
        }

    }
}
