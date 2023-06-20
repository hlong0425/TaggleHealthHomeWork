using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Application.Books;
using System;

namespace API.Controllers
{
    [AllowAnonymous]
    public class BooksController : BaseApiController
    {
        private IMediator _mediator;


        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetActivities()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }


        [HttpPut("{id}/register")]
        public async Task<ActionResult<List<Book>>> RegisterBook(Guid bookId)
        {
            return HandleResult(await Mediator.Send(new Register.Command { BookId = bookId }));
        }
    }
}