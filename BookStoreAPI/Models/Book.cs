namespace BookStoreAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
