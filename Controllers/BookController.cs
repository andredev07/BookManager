using BookstoreManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public static readonly List<Book> _books = new List<Book>();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Book book)
        {
            if (book.Price <= 0 || book.Stock <= 0) 
            {
                return BadRequest(new
                {
                    error = "Validação falhou!",
                    Message = "O preço ou estoque não pode ser menor que 0!"
                });
            }

            book.Id = Guid.NewGuid();

            _books.Add(book);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_books);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var response = _books.FirstOrDefault(x => x.Id == id);

            if (response == null)
            {
                return NotFound(new { message = "Livro não encontrado" });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(Guid id, [FromBody] Book updateBook)
        {
            if (id != updateBook.Id)
            {
                return BadRequest("Livro não encontrado");
            }

            if (updateBook.Price <= 0 && updateBook.Stock <= 0)
            {
                return BadRequest(new
                {
                    error = "Validação falhou!",
                    Message = "O preço ou estoque não pode ser menor que 0!"
                });
            }

            var bookFound = _books.FirstOrDefault(x => x.Id == id);

            bookFound.Title = updateBook.Title;
            bookFound.Author = updateBook.Author;
            bookFound.Genre = updateBook.Genre;
            bookFound.Price = updateBook.Price;
            bookFound.Stock = updateBook.Stock;


            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var response = _books.FirstOrDefault(x => x.Id == id);

            if(response == null)
            {
                return NotFound("Livro não encontrado");
            }

            _books.Remove(response);

            return NoContent();
        }
    }
}
