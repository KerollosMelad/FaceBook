using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceBook_InitialVersion.Models
{
    public class PersonModelView
    {
        public List<Post> MyPosts;
        public List<Post> FriendPosts;
        public List<Person> myFriends;
        public List<Person> myFriendsRequest;
        public Person CurrentUser;
    }
}
