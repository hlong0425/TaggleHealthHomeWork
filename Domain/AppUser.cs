using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public double Credits { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}