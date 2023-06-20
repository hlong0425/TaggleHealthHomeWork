using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Books
{
    public class Register
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid BookId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _context.Books
                .Include(a => a.User)
                .SingleOrDefaultAsync(x => x.Id == request.BookId);

                if (book == null) return null;

                if (book.User.Id != null)
                {
                    return Result<Unit>.Failure("Sách đã được đăng kí, vui vòng chọn sách khác.");
                }

                var currentUser = await _context.Users
                .Include(user => user.Books)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserId());

                if (currentUser == null) return null;

                if (currentUser.Credits < book.Credits)
                {
                    return Result<Unit>.Failure("Đăng kí thất bại, tài khoản của bạn không đủ để đăng kí sách.");
                }

                if (currentUser.Books.Count > 5)
                {
                    return Result<Unit>.Failure("Đăng kí thất bại, mỗi tài khoản chỉ có thể đăng kí tối đa 3 cuốn sách.");
                }

                currentUser.Credits = currentUser.Credits - book.Credits;

                book.User = currentUser;
                book.ExpirationDate = DateTime.Now.AddDays(30); // Default ExpirationDate = 30

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Something went wrong");
            }
        }
    }
}