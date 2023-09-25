using BookStoreAPI.Models;

namespace BookStoreAPI.IServices
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(int id, Author updatedAuthor);
        Task<bool> DeleteAuthor(int id);
    }
}
