using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Member:Person
    {
        public Member()
        {

        }
        public Member(string name):base(name)
        {

        }

        [Required]
        public UserStatus State { get; set; }

        public string Bio { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<UserPostLike> UserPostLikes { get; set; }
        public virtual List<UserPostComment> UserPostComments { get; set; }


    }
}
