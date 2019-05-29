using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 to 8 charactor")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}