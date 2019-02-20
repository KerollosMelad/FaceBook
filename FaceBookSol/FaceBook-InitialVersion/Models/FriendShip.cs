using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Friendship
    {
        [Key]
        public int ID { get; set; }
        public FriendShipStatus friendShipStatus { get; set; }

        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>

        public int FriendID { get; set; }

        public virtual Member User { get; set; }

    }
}
