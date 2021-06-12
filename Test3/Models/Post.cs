using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Type? Type { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Title must be below 32 characters long")]
        public string Title { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Description must be below 32 characters long")]
        public string Description { get; set; }
        [Url(ErrorMessage ="Invalid URL")]
        public string PhotoUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}