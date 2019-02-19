using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class Friendship
    {
        public string state { get; set; }

        [ForeignKey("Friend")]
        public string _friendID { get; set; }

        [ForeignKey("User")]
        public string _userID { get; set; }


        public virtual Member User { get; set; }
        public virtual Member Friend { get; set; }
    }
}
