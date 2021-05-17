using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public enum Size
    {
        small, average, big
    }

    public enum Difficulty
    {
        easy, moderate, hard
    }

    public enum Sex
    {
        male, female
    }
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Sex? Sex { get; set; }
        public int Origins { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Size? Size { get; set; }
        public Difficulty? Difficulty { get; set; }
        public string PhotoUrl { get; set; }

        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}

