using BookStoreAPI.Controllers;
using BookStoreAPI.IServices;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class BooksControllerTests
    {
        private Mock<IBookService> _bookServiceMock;
        private BooksController _booksController;

        [SetUp]
        public void Setup()
        {
            _bookServiceMock = new Mock<IBookService>();
            _booksController = new BooksController(_bookServiceMock.Object);
        }

        [Test]
        public async Task GetAll_Returns_OkObjectResult()
        {
            // Arrange
            _bookServiceMock.Setup(service => service.GetAllBooks())
                .ReturnsAsync(GetTestBooks());

            // Act
            var result = await _booksController.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetById_ExistingId_Returns_OkObjectResult()
        {
            // Arrange
            int id = 1;
            _bookServiceMock.Setup(service => service.GetBookById(id))
                .ReturnsAsync(GetTestBooks()[0]);

            // Act
            var result = await _booksController.GetById(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetById_NonExistingId_Returns_NotFoundResult()
        {
            // Arrange
            _bookServiceMock.Setup(service => service.GetBookById(It.IsAny<int>()))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _booksController.GetById(2);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        private List<Book> GetTestBooks()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Test Book 1", AuthorId = 1, GenreId = 1, Price = 9.99m, QuantityAvailable = 10 },
                new Book { BookId = 2, Title = "Test Book 2", AuthorId = 2, GenreId = 1, Price = 14.99m, QuantityAvailable = 5 }
            };
            return books;
        }
    }
}
