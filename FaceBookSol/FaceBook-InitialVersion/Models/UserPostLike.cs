using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class UserPostLike
    {
        [ForeignKey("Post")]
        public int PostID { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }

        public virtual Post Post { get; set; }

        public virtual Person User { get; set; }
    }
}
