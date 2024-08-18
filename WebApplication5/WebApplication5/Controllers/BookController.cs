using Microsoft.AspNetCore.Mvc;
using WebApplication5.Repositories;

namespace WebApplication5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository  _bookRepository;

        public BookController(IBookRepository bookRepo)
        {
            _bookRepository = bookRepo;
        }


        [HttpPost("Kitap Onay")]
        public async Task<IActionResult> ApproveBook([FromQuery] int bookId, [FromQuery] int staffId)
        {
            var success = await _bookRepository.ApproveBookasync(bookId, staffId);
            if (!success)
            {
                return NotFound("Book or staff not found");
            }

            return Ok("Book approved successfully");
        }


        [HttpPost("Kitap Red")]
        public async Task<IActionResult> RejectedBook([FromQuery] int bookId, [FromQuery] int staffId)
        {
            var success = await _bookRepository.RejectedBookasync(bookId, staffId);
            if (!success)
            {
                return NotFound("Book or staff not found");
            }

            return Ok("Book rejected");
        }





    }
}
