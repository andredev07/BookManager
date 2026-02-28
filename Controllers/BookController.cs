using BookstoreManager.Dto;
using BookstoreManager.Enums;
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
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] BookDto bookDto)
        {
            if (!Enum.TryParse<BookGenre>(bookDto.Genre, true, out var genreResult))
            {   
                return BadRequest(new
                {
                    Error = $"O gênero {bookDto.Genre} é inválido.",
                    OpcoesValidas = Enum.GetNames(typeof(BookGenre)),
                });
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = genreResult,
                Price = bookDto.Price,
                Stock = bookDto.Stock
            };

            _books.Add(book);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            if (!_books.Any())
            {
                return NotFound("Não existe nenhum livro!");
            }

            return Ok(_books);
        }  

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Book),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var response = _books.FirstOrDefault(x => x.Id == id);

            if (response == null)
            {
                return NotFound(new { message = "Livro não encontrado!" });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, [FromBody] BookDto updateBook)
        {
            var bookFound = _books.FirstOrDefault(x => x.Id == id);

            if (bookFound== null)
            {
                return NotFound("Livro não encontrado");
            }

            if (updateBook.Price <= 0 && updateBook.Stock <= 0)
            {
                return BadRequest(new
                {
                    error = "Validação falhou!",
                    Message = "O preço ou estoque não pode ser menor que 0!"
                });
            }

            Enum.TryParse<BookGenre>(updateBook.Genre, true, out var genreResult);

            bookFound.Title = updateBook.Title;
            bookFound.Author = updateBook.Author;
            bookFound.Genre = genreResult;
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
