using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class Enums
    {
        public enum Gender
        {
            Female,
            Male
        }

        public enum UserStatus
        {
            Blocked,
            Active
        }

        public enum FriendShipStatus
        {
            Pending,
            Accepted,
            Declined
        }

        public enum PostStatus
        {
            Deleted,
            Active
        }

        public enum CommentStatus
        {
            Deleted,
            Active
        }
    }
}
