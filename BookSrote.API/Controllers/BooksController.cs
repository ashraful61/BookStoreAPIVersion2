using BookSrote.API.Models;
using BookSrote.API.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookSrote.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        //Get all books
        [HttpGet("")]
        public async Task<IActionResult> GetAllBookds()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        //Get book by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //Add a new book
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.AddBookAsync(bookModel);
        
            return CreatedAtAction(nameof(GetBookById), new { id = id, controller="books"}, id);
        }

        //Update book by id
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody]BookModel bookModel)
        {
            await _bookRepository.UpdateBookByIdAsync(id, bookModel);
            return Ok();
        }


        //Patch book by id
        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument bookModel)
        {
            await _bookRepository.UpdateBookPatchAsync(id, bookModel);
            return Ok();
        }


        //Update book by id
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DelecteBook([FromRoute] int id, [FromBody] BookModel bookModel)
        {
            await _bookRepository.DeleteBookByIdAsync(id);
            return Ok();
        }

    }
}
