using System;
using System.Collections.Generic;
using BusinessApp.API.Models;

namespace BusinessApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string ContactNumber { get; set; }
        public int Age { get; set; }
        public ICollection<BusinessForDetailedDto> Businesses { get; set; }
    }
}