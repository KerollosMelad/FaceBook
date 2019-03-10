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
            Declined,
            PendingFriendAccept         // if you sent request and waiting ... 

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

        public enum UserType
        {
            User,
            Admin
            
        }

        

    }
}
