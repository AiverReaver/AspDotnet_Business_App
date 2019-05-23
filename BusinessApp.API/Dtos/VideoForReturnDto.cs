using System;

namespace BusinessApp.API.Dtos
{
    public class VideoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}