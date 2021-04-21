using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Origins { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Difficulty { get; set; }
        public int ShelterId { get; set; }
        public string PhotoUrl { get; set; }
        public virtual Shelter Shelter { get; set; }
    }
}
