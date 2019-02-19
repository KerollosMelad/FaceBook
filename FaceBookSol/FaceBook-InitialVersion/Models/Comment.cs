using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public CommentStatus State { get; set; }

        public int UserPostCommentID { get; set; }
        // comment won't repeat in UserPostComment
        // so relation between UserPostComment and comment
        //is one to one 
        public virtual UserPostComment UserPostComments { get; set; }


        


    }
}
