
using Application.Core;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Books
{
    public class List : IRequest
    {
        public class Query : IRequest<Result<List<BookDto>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<BookDto>>>
        {
            private readonly DataContext _context;

            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<BookDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Ng dùng chỉ được thấy sách mình đã mượn, ko thấy được sách của người khác mượn
                // hoặc sách đang available
                var books = await _context.Books
                    .Include(b => b.User)
                    .Where(b => (b.UserId == "") || (b.UserId == _userAccessor.GetUserId()))
                    .ToListAsync();

                var currentDay = DateTime.UtcNow;

                var bookDtos = books.Select(b =>
                {
                    return new BookDto
                    {
                        Title = b.Title,
                        Credits = b.Credits,
                        ExpirationDate = b.ExpirationDate,
                        RemainingDay = b.ExpirationDate != null ? (b.ExpirationDate.Value - currentDay).Days : null,
                        UserName = b.User != null ? b.User.UserName : null,
                    };
                }).ToList();

                return Result<List<BookDto>>.Success(bookDtos);
            }
        }
    }
}