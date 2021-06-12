using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class ClinicRatingModel
    {
        [Required]
        public Clinic Clinic { get; set; }
        [Required]
        public Rating Rating { get; set; }
        public IEnumerable<Clinic> Clinics{ get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

    }
}
