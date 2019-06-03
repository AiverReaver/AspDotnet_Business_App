using System.Collections.Generic;

namespace BusinessApp.API.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string OfficeNumber { get; set; }
        public string Landmark { get; set; }
        public bool IsPublished { get; set; }
        public bool IsPublishable { get; set; } 
        public User User { get; set; }
        public int UserId { get; set; }
        public Video Video { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}