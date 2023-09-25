using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetGenreById(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            var createdGenre = await _genreService.AddGenre(genre);
            return CreatedAtAction(nameof(GetById), new { id = createdGenre.GenreId }, createdGenre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Genre updatedGenre)
        {
            var genre = await _genreService.UpdateGenre(id, updatedGenre);
            if (genre == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _genreService.DeleteGenre(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
