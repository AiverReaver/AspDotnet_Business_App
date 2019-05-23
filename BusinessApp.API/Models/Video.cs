using System;

namespace BusinessApp.API.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public Business Business { get; set; }
        public int BusinessId { get; set; }
    }
}