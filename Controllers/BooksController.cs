using BookstoreManeger.Entities;
using BookstoreManeger.Request;
using BookstoreManeger.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManeger.Controllers;

[ApiController]
public class BooksController : BookstoreManegerControllerBase
{
    private static readonly List<Book> _books = new();


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookResponseJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        var response = _books.Select(book => new BookResponseJson
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            Stock = book.Stoke,
            Created_At = book.Created_At,
            Updated_At = book.Updated_At,
        });
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(BookResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Livro não encontrado",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Não existe livro com o id {id}"
            });
        }

        var response = new BookResponseJson
        {
            Id = book.Id,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            Stock = book.Stoke,
            Title = book.Title,
            Created_At = book.Created_At,
            Updated_At = book.Updated_At,
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] BookRequestJson request)
    {
        
        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.Author))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Dados inválidos",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Título e autor são obrigatórios"
            });
        }
        if (request.Price <= 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "O preço precisa ser maior que 0"
            });
        }
        if (request.Stock <= 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "O estoque precisa ser maior que 0"
            });
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stoke = request.Stock,
            Genre = request.Genre,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,

        };

        if (request.Price <= 0)
        {
            return BadRequest("Preço precisa ser maior que 0");
        }
        _books.Add(book);

        var response = new BookResponseJson

        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            Stock = book.Stoke,
            Created_At = DateTime.Now,

        };

        return Created();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var book = _books.FirstOrDefault(book => book.Id == id);
        if (book == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Livro não encontrado",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Não existe livro com o id {id}"
            }); 
        }
        _books.Remove(book);
        return NoContent();
    }
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(BookResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Edit(Guid id, [FromBody]UpdateBookRequestJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.Author))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Dados inválidos",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Título e autor são obrigatórios"
            });
        }
        if (request.Price <= 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "O preço precisa ser maior que 0"
            });
        }
        if (request.Stock <= 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "O estoque precisa ser maior que 0"
            });
        }

        var book = _books.FirstOrDefault(b => b.Id == id);
        if(book == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Livro não encontrado",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Não existe livro com o id {id}"
            });
        }
        book.Title = request.Title;
        book.Author = request.Author;
        book.Updated_At = DateTime.UtcNow;

        var response = new BookResponseJson
        {
            Id = book.Id,
            Title= book.Title,
            Author= book.Author,
            Genre= book.Genre,
            Price= book.Price,
            Stock=book.Stoke,
            Created_At=book.Created_At,
            Updated_At= book.Updated_At,
        };
        return Ok(response);
    }
}
