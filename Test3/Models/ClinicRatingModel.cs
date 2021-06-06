using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class ClinicRatingModel
    {
        public Clinic Clinic { get; set; }
        public Rating Rating { get; set; }
        public IEnumerable<Clinic> Clinics{ get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

    }
}
