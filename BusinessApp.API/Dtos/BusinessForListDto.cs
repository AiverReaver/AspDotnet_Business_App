namespace BusinessApp.API.Dtos
{
    public class BusinessForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public int UserId { get; set; }
    }
}