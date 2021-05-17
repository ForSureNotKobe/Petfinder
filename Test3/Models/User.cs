using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class User : IdentityUser
    {
        public string Nip { get; set; }
        public List<Post> Posts { get; set; }
        public List<Rating> Ratings { get; set; }

        public int? ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}

