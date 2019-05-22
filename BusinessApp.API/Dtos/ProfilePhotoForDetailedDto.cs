using System;

namespace BusinessApp.API.Dtos
{
    public class ProfilePhotoForDetailedDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
    }
}