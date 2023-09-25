using BookStoreAPI.Models;

namespace BookStoreAPI.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(int id, Book updatedBook);
        Task<bool> DeleteBook(int id);
        Task<IEnumerable<Book>> SearchBooks(string title, string author, string genre);
    }
}
