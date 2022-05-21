using BookShop.DataProviders;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.Controllers.Tests
{
    public class BooksControllerTests
    {
        private IEnumerable<Book> GetBooks()
        {
            yield return new Book()
            {
                BookCode = 1,
                AuthorCode = 1,
                GenreCode = 1,
                PublishingCode = 1,
                YearOfWriting = 1999,
                YearOfPublishing = 2000,
                Title = "Poem",
                Price = 100
            };

            yield return new Models.Book()
            {
                BookCode = 2,
                AuthorCode = 2,
                GenreCode = 2,
                PublishingCode = 2,
                YearOfWriting = 2000,
                YearOfPublishing = 2001,
                Title = "Book2",
                Price = 200
            };
        }

        private IEnumerable<Genre> GetGenres()
        {
            yield return new Genre()
            {
                GenreCode = 1,
                DescriptionGenre = "111",
                DiscountCode = null,
                Title = "Poem",
            };

            yield return new Genre()
            {
                GenreCode = 2,
                DescriptionGenre = "222",
                DiscountCode = null,
                Title = "Drama",
            };
        }

        private IEnumerable<Author> GetAuthors()
        {
            yield return new Author()
            {
                AuthorCode = 1,
                Fio = "A A A",
                YearOfBirth = 1888,
                YearOfDeath = 1955,
                Country = "Russia",
            };

            yield return new Author()
            {
                AuthorCode = 2,
                Fio = "B B B",
                YearOfBirth = 1555,
                YearOfDeath = 1600,
                Country = "Germany",
            };
        }
        private IEnumerable<Publishing> GetPublishings()
        {
            yield return new Publishing()
            {
                PublishingCode = 1,
                Title = "O O O",
                City = "Moscow",
            };

            yield return new Publishing()
            {
                PublishingCode = 2,
                Title = "C C C",
                City = "Ryazan",
            };
        }
        private BooksController CreateController(int showedAmount)
        {
            Mock<IDataProvider<Book>> bookService = new Mock<IDataProvider<Book>>();
            Mock<IDataProvider<Genre>> genreService = new Mock<IDataProvider<Genre>>();
            Mock<IDataProvider<Author>> authorService = new Mock<IDataProvider<Author>>();
            Mock<IDataProvider<Publishing>> publishingService = new Mock<IDataProvider<Publishing>>();
            Mock<ServiceProducts> productsAmount = new Mock<ServiceProducts>();
            bookService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetBooks().ToList(); }));
            genreService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetGenres().ToList(); }));
            authorService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetAuthors().ToList(); }));
            publishingService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetPublishings().ToList(); }));
            var controller = new BooksController(bookService.Object, genreService.Object, authorService.Object, publishingService.Object, new ServiceProducts(showedAmount));
            return controller;
        }
        [Fact]
        public async Task TestIndexAsync_TwoBooks_Success()
        {
            // Arrange
            BooksController controller = CreateController(10);

            //Act

            var result = await controller.IndexAsync(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task TestIndexAsync_OneBook_Success()
        {
            // Arrange
            BooksController controller = CreateController(1);

            //Act

            var result = await controller.IndexAsync(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }

        

        [Fact]
        public async Task TestIndexAsync_SelectedGenreBook_Success()
        {
            // Arrange
            BooksController controller = CreateController(10);

            //Act

            var result = await controller.IndexAsync(1);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }
        
        [Fact]
        public async Task TestCreateAsync_NewBook_Success()
        {
            Book newBook = new Book()
            {
                BookCode = 3,
                AuthorCode = 2,
                GenreCode = 2,
                PublishingCode = 2,
                YearOfWriting = 2005,
                YearOfPublishing = 2011,
                Title = "Book3",
                Price = 400
            };
            Mock<IDataProvider<Book>> bookService = new Mock<IDataProvider<Book>>();
            Mock<IDataProvider<Genre>> genreService = new Mock<IDataProvider<Genre>>();
            Mock<IDataProvider<Author>> authorService = new Mock<IDataProvider<Author>>();
            Mock<IDataProvider<Publishing>> publishingService = new Mock<IDataProvider<Publishing>>();
            Mock<ServiceProducts> productsAmount = new Mock<ServiceProducts>();
            bookService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetBooks().ToList(); }));
            bookService.Setup(book => book.Get(3)).Returns(newBook);
            genreService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetGenres().ToList(); }));
            authorService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetAuthors().ToList(); }));
            publishingService.Setup(repo => repo.GetListAsync()).Returns(Task.Run(() => { return GetPublishings().ToList(); }));
            var controller = new BooksController(bookService.Object, genreService.Object, authorService.Object, publishingService.Object, new ServiceProducts(10));

            Book book = new Book()
            {
                BookCode = 3,
                AuthorCode = 2,
                GenreCode = 2,
                PublishingCode = 2,
                YearOfWriting = 2005,
                YearOfPublishing = 2011,
                Title = "Book3",
                Price = 400
            };

            bookService.Setup(repo => repo.EditAsync(newBook)).Returns(Task.CompletedTask).Verifiable();
            //Act

            var result = await controller.CreateAsync(book);

            //Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var redirectActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Book>(redirectActionResult);
            bookService.Verify();
            var booksList = await bookService.Object.GetListAsync();
            Assert.Equal(3, booksList.Count);
            /*Assert.Equal(newBook.BookCode, returnValue.BookCode);
            Assert.Equal(newBook.Title, returnValue.Title);*/
        }
        /*
        [Test]
        public void TestCreate()
        {
            Assert.Fail();
        }

        [Test]
        public void TestCreateAsync()
        {
            Assert.Fail();
        }

        [Test]
        public void TestEditAsync()
        {
            Assert.Fail();
        }

        [Test]
        public void TestEditPostAsync()
        {
            Assert.Fail();
        }

        [Test]
        public void TestDelete()
        {
            Assert.Fail();
        }

        [Test]
        public void TestDelete1()
        {
            Assert.Fail();
        }*/
    }
}
