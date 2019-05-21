using System.Collections.Generic;
using BusinessApp.API.Models;

namespace BusinessApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<BusinessesForDetailedDto> Businesses { get; set; }
    }
}