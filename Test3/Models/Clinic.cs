using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Nip { get; set; }

        public List<Rating> Ratings { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
