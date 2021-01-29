using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrack.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")] 
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Descripton { get; set; }
    }
}
