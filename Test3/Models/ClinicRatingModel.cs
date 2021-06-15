using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Petfinder.Models
{
    public class ClinicRatingModel
    {
        [Required]
        public Clinic Clinic { get; set; }
        [Required]
        public Rating Rating { get; set; }
        public IEnumerable<Clinic> Clinics { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

    }
}
