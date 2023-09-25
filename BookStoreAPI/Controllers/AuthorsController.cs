using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            var createdAuthor = await _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.AuthorId }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Author updatedAuthor)
        {
            var author = await _authorService.UpdateAuthor(id, updatedAuthor);
            if (author == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authorService.DeleteAuthor(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
