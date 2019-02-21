using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Person: IdentityUser
    {
        public Person()
        {

        }

        public Person(string name) : base(name)
        {

        }

        ///// <summary>
        /////  admin Or User
        ///// </summary>
        //public UserType Type { get; set; }


        [Required]
        public UserStatus State { get; set; }

        public string Bio { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
 

        [Required]
        public Gender Gender { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual List<Friendship> FriendRequestMade { get; set; }
        public virtual List<Friendship> FriendRequestAccepted { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<UserPostLike> UserPostLikes { get; set; }
        public virtual List<UserPostComment> UserPostComments { get; set; }
    }
}
