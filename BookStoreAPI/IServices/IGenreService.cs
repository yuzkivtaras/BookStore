using BookStoreAPI.Models;

namespace BookStoreAPI.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(int id);
        Task<Genre> AddGenre(Genre genre);
        Task<Genre> UpdateGenre(int id, Genre updatedGenre);
        Task<bool> DeleteGenre(int id);
    }
}
