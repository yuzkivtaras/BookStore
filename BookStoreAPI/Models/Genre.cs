﻿namespace BookStoreAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
