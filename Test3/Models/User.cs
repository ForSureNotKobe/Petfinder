﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Petfinder.Models
{
    public class User : IdentityUser
    {
        public string Nip { get; set; }
        public List<Post> Posts { get; set; }
        public List<Rating> Ratings { get; set; }

        public int? ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public int? ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}

