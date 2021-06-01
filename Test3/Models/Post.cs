using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public enum Type
    {
        lost, found
    }

    public class Post
    {        
        public int PostId { get; set; }
        public Type? Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}