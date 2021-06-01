using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Nip { get; set; }

        public List<Pet> Pets { get; set; }
        public List<User> Users { get; set; }

        public IEnumerable<Shelter> Shelters { get; set; }
    }
}