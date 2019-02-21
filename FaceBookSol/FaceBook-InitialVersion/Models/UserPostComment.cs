using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class UserPostComment
    {
        //[Key]
        [ForeignKey("Comment")]
        //[Column(Order = 0)]
        public int CommentID { get; set; }

        //[Key]
        [ForeignKey("Post")]
        //[Column(Order = 1)]
        public int PostID { get; set; }

        //[Key]
        [ForeignKey("User")]
        //[Column(Order = 2)]
        public string userID { get; set; }




        public virtual Comment Comment { get; set; }

        public virtual Post Post { get; set; }

        public virtual Person User { get; set; }
    }
}
