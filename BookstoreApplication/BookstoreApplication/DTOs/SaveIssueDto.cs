namespace BookstoreApplication.DTOs
{
    public class SaveIssueDto
    {
        public int ExternalApiId { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
    }
}
