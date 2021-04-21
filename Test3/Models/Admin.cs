using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Vet> Vets { get; set; }
        public virtual List<Shelter> Shelters { get; set; }
        public virtual List<Ratings> Ratings { get; set; }
        public virtual List<Pet> Pets { get; set; }
    }
}
