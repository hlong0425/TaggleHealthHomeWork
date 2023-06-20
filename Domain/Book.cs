namespace Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Credits { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}