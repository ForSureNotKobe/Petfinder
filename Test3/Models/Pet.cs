using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public enum Origins
    {
        shelterBorn, found, given
    }

    public enum BreedType
    {
        dog, cat, other
    }
    public class Pet
    {
        public int PetId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Sex? Sex { get; set; }
        [Required]
        public Origins? Origins { get; set; }
        [Required]
        public BreedType? BreedType { get; set; }
        [Required]
        public string Description { get; set; }
        public Size? Size { get; set; }
        public Difficulty? Difficulty { get; set; }
        public string PhotoUrl { get; set; }

        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}

