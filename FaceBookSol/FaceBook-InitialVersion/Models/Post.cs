using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [Required]
        public PostStatus State { get; set; }

        public virtual List<UserPostComment> UserPostComments { get; set; }
        public virtual List<UserPostLike> UserPostLikes { get; set; }
    }
}
