using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string Rating { get; set; }
        public virtual Vet Vet { get; set; }
        public virtual User User { get; set; }
    }
}
