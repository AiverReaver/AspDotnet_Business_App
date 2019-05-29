using System.Collections.Generic;
using BusinessApp.API.Models;

namespace BusinessApp.API.Dtos
{
    public class BusinessForDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string OfficeNumber { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailedDto> Photos { get; set; }
        public VideoForReturnDto Video { get; set; }
    }
}