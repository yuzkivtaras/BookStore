using BookStoreAPI.Data;
using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Service
{
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext _context;

        public BookService(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(int id, Book updatedBook)
        {
            if (updatedBook == null) throw new ArgumentNullException(nameof(updatedBook));

            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null) return null;

            existingBook.Title = updatedBook.Title;
            existingBook.AuthorId = updatedBook.AuthorId;
            existingBook.GenreId = updatedBook.GenreId;
            existingBook.Price = updatedBook.Price;
            existingBook.QuantityAvailable = updatedBook.QuantityAvailable;

            _context.Entry(existingBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null) return false;

            _context.Books.Remove(existingBook);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooks(string title, string author, string genre)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.Author.Name.Contains(author));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(b => b.Genre.Name.Contains(genre));
            }

            return await query.ToListAsync();
        }
    }
}
