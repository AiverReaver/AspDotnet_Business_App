using System.ComponentModel.DataAnnotations;

namespace BusinessApp.API.Dtos
{
    public class BusinessForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "You must specify OfficeNumber between 10 to 14 character")]
        public string OfficeNumber { get; set; }

        [Required]
        public string Landmark { get; set; }
    }
}