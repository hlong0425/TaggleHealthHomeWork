using System;
using System.Linq;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace API.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public IConfiguration _config;
        public DataContext _dataContext;
        public BackgroundJobService(IConfiguration config, DataContext dataContext)
        {
            _config = config;
            _dataContext = dataContext;
        }
        public async void HandleExpiredBook()
        {
            var currentDay = DateTime.UtcNow;
            var expiredBooks = await _dataContext.Books
                .Include(book => book.User)
                .Where(book => book.ExpirationDate.HasValue && book.ExpirationDate.Value.Date < currentDay.Date)
                .ToListAsync();

            var users = expiredBooks.Select(b => b.User);
            foreach (var user in users)
            {
                user.Credits = user.Credits * 20 / 100;
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}