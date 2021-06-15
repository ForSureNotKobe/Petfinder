using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        [Required]
        [StringLength(32,ErrorMessage = "Name should have between 3 and 32 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail adress")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9-+]{9,15}$", ErrorMessage = "Invalid phone number")]
        public int PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(1000000000,9999999999, ErrorMessage = "Invalid NIP number")]
        public int Nip { get; set; }

        public List<Rating> Ratings { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
