using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public enum Opinion
    {
        bad, neutral, good
    }
    public class Rating
    {
        public int RatingId { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Description must be below 32 characters long")]
        public string Content { get; set; }
        [Required]
        public Opinion? Opinion { get; set; }        
        public Clinic Clinic { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int ClinicId { get; set; }
    }
}

