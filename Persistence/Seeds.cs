using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public static class Seeds
    {
        public static async Task SeedData(DataContext context,
            IConfiguration config,
            UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any() && !context.Books.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Long",
                    UserName = "long",
                    Email = "long@gmail.com",
                    Credits = 100.00,
                };

                await userManager.CreateAsync(user, "password");

                var bookQuantity = int.Parse(config.GetSection("defaultBookQuantity").Value);

                var book = DummyData.CreateListBooks(bookQuantity);
                await context.Books.AddRangeAsync(book);
                await userManager.CreateAsync(user);

                await context.SaveChangesAsync();
            }
        }
    }
}
