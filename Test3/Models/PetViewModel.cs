using System.Collections.Generic;

namespace Petfinder.Models
{
    public class PetViewModel
    {
        public IEnumerable<Pet> Pets { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

    }
}
