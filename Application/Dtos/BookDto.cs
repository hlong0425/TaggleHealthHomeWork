namespace Application.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Credits { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsExpired { get; set; }
        public string UserName { get; set; }
        public double? RemainingDay { get; set; }
    }
}