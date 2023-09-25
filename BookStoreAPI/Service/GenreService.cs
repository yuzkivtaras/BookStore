using BookStoreAPI.Data;
using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Service
{
    public class GenreService : IGenreService
    {
        private readonly BookStoreDbContext _context;

        public GenreService(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            if (genre == null) throw new ArgumentNullException(nameof(genre));

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> UpdateGenre(int id, Genre updatedGenre)
        {
            if (updatedGenre == null) throw new ArgumentNullException(nameof(updatedGenre));

            var existingGenre = await _context.Genres.FindAsync(id);

            if (existingGenre == null) return null;

            existingGenre.Name = updatedGenre.Name;

            _context.Entry(existingGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingGenre;
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var existingGenre = await _context.Genres.FindAsync(id);

            if (existingGenre == null) return false;

            _context.Genres.Remove(existingGenre);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
