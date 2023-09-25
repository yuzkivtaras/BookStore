using BookStoreAPI.Data;
using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly BookStoreDbContext _context;

        public AuthorService(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthor(int id, Author updatedAuthor)
        {
            if (updatedAuthor == null) throw new ArgumentNullException(nameof(updatedAuthor));

            var existingAuthor = await _context.Authors.FindAsync(id);

            if (existingAuthor == null) return null;

            existingAuthor.Name = updatedAuthor.Name;

            _context.Entry(existingAuthor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingAuthor;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);

            if (existingAuthor == null) return false;

            _context.Authors.Remove(existingAuthor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
