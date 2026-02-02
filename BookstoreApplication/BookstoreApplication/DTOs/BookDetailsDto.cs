namespace BookstoreApplication.DTOs
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public int ReleaseDate { get; set; } 
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public int PublisherId { get; set; }
        public int HowOldIsBook { get; set; }
    }
}
