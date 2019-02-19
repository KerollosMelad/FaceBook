using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Models
{
    public class Person: IdentityUser
    {
        public Person()
        {

        }

        public Person(string name) : base(name)
        {

        }

        public DateTime BirthDay { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime CreationDate { get; set; }

        //public virtual List<FriendShip> FriendRequestMade { get; set; }
        //public virtual List<FriendShip> FriendRequestAccepted { get; set; }
    }
}
