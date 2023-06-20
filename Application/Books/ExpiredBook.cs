using MediatR;
using Persistence;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.ExpiredBook
{
    public class ExpiredBook : IRequest
    {
        public class Query : IRequest<Unit>
        {
        }

        public class Handler : IRequestHandler<Query, Unit>
        {
            private DataContext _context;
            private IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                // Cứ 1 ngày qúa hạn người dùng mất 20% credit của mỗi sách
                var currentDay = DateTime.UtcNow;
                var expiredBooks = await _context.Books
                    .Include(book => book.User)
                    .Where(book => book.ExpirationDate.HasValue && book.ExpirationDate.Value.Date < currentDay.Date)
                    .ToListAsync();

                var users = expiredBooks.Select(b => b.User);
                foreach (var user in users)
                {
                    user.Credits = user.Credits * 20 / 100;
                }

                var result = await _context.SaveChangesAsync() > 0;

                return Unit.Value;
            }
        }
    }
}