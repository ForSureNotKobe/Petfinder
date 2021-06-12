using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail adress")] 
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Nip { get; set; }

        public List<Pet> Pets { get; set; }
        public List<User> Users { get; set; }

        public IEnumerable<Shelter> Shelters { get; set; }
    }
}