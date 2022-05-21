using BookShop.DataProviders;
using BookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        //private readonly Models.BookShopContext context;
        private List<Breadcrumb> breadcrumbs;
        private readonly int showedAmount;
        private readonly IDataProvider<Book> booksProvider;
        private readonly IDataProvider<Genre> genresProvider;
        private readonly IDataProvider<Author> authorsProvider;
        private readonly IDataProvider<Publishing> publishongProvider;
        private readonly IDataProvider<BooksImage> imagesProvider;

        public BooksController(IDataProvider<Book> booksProvider, IDataProvider<Genre> genresProvider, IDataProvider<Author> authorsProvider,
            IDataProvider<Publishing> publishongProvider, IDataProvider<BooksImage> imagesProvider, ServiceProducts serviceProducts)
        {
            //this.context = context;
            this.booksProvider = booksProvider;
            this.genresProvider = genresProvider;
            this.authorsProvider = authorsProvider;
            this.imagesProvider = imagesProvider;
            this.publishongProvider = publishongProvider;
            showedAmount = serviceProducts.Amount;

            breadcrumbs = new List<Breadcrumb>()
            {
                new Breadcrumb("Welcome", "Index")
            };
        }
        // GET: BooksController

        private void AddBreadcrumb(string controller, string action, int position)
        {
            if(breadcrumbs.Count - 1 < position)
            {
                breadcrumbs.Add(new Breadcrumb(controller, action));
            }
            else
            {
                breadcrumbs[position] = new Breadcrumb(controller, action);
            }

            ViewBag.Breadcrumbs = breadcrumbs;
        }

        public async Task<ActionResult> IndexAsync(int? genreId)
        {
            var books = await booksProvider.GetListAsync();
            AddBreadcrumb("Books", "Index", 1);
            if (genreId.HasValue)
            {
                return View(books.Where(x => x.GenreCode == genreId));
            }

            if (showedAmount > 0)
            {
                return View(books.Take(showedAmount));
            }

            
            return View(books);
        }

        [Route("images/{id}")]
        public async Task<ActionResult> Image(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = imagesProvider.Get(id.Value);

            if (image == null)
            {
                return NotFound();
            }

            return File(image.Img, "image/png");
        }

        public async Task<ActionResult> EditImage(int? id)
        {
            AddBreadcrumb("Books", "Index", 1);
            AddBreadcrumb("Books", "EditImage", 2);
            if (id == null)
            {
                return NotFound();
            }

            var image = imagesProvider.Get(id.Value);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost]
        public async Task<ActionResult> EditImage([Bind("Id,Img,ImageFile")] BooksImage image)
        {
            if (ModelState.IsValid)
            {
                image.CopyImageAsync();
                await imagesProvider.EditAsync(image);
            }

            return RedirectToAction("Index");
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create
        public async Task<ActionResult> CreateAsync()
        {
            AddBreadcrumb("Books", "Index", 1);
            AddBreadcrumb("Books", "Create", 2);
            await GenresDropDownListAsync();
            await AuthorsDropDownListAsync();
            await PublishingDropDownListAsync();
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Book>> CreateAsync([Bind("AuthorCode,GenreCode,PublishingCode,YearOfWriting,YearOfPublishing,Title,Price")] Book book)
        {
            CheckBook(book);
            List<Book> books = await booksProvider.GetListAsync();
            if (books.Select(x => x.Title).Contains(book.Title))
            {
                ModelState.AddModelError("Title", "This book already exists");
            }

            if (ModelState.IsValid)
            {
                await booksProvider.CreateAsync(book);
                return RedirectToAction(nameof(Index));
            }

            await GenresDropDownListAsync();
            await AuthorsDropDownListAsync();
            await PublishingDropDownListAsync();
            return CreatedAtAction(nameof(CreateAsync), book);
        }

        private void CheckBook(Book book)
        {
            if (book.YearOfWriting > book.YearOfPublishing)
            {
                ModelState.AddModelError("YearOfPublishing", "Year of publishing cannot be greater than year of writing");
            }

            if (book.YearOfWriting > DateTime.Now.Year)
            {
                ModelState.AddModelError("YearOfWriting", "Year of writing cannot be greater than current year");
            }

            if (book.YearOfPublishing > DateTime.Now.Year)
            {
                ModelState.AddModelError("YearOfPublishing", "Year of publishing cannot be greater than current year");
            }
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var book = booksProvider.Get(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            AddBreadcrumb("Books", "Index", 1);
            AddBreadcrumb("Books", "Edit", 2);
            await GenresDropDownListAsync(book.GenreCode);
            await AuthorsDropDownListAsync(book.AuthorCode);
            await PublishingDropDownListAsync(book.PublishingCode);
            return View(book);
        }

        // POST: BooksController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPostAsync(Book book)
        {
            CheckBook(book);

            if (ModelState.IsValid)
            {
                try
                {
                    await booksProvider.EditAsync(book);
                }
                catch
                {
                    return View(book);
                }
            }

            await GenresDropDownListAsync(book.GenreCode);
            await AuthorsDropDownListAsync(book.AuthorCode);
            await PublishingDropDownListAsync(book.PublishingCode);
            return View(book);
        }

        private async Task GenresDropDownListAsync(int? selected = null)
        {
            var genres = await genresProvider.GetListAsync();
            var selectedItem = selected.HasValue ? genres.Find(x => x.GenreCode == selected) : null;
            ViewBag.GenreID = new SelectList(genres, "GenreCode", "Title", selectedItem);
        }

        private async Task AuthorsDropDownListAsync(int? selected = null)
        {
            var authors = await authorsProvider.GetListAsync();
            var selectedItem = selected.HasValue ? authors.Find(x => x.AuthorCode == selected) : null;
            ViewBag.AuthorsID = new SelectList(authors, "AuthorCode", "Fio", selectedItem);
        }

        private async Task PublishingDropDownListAsync(int? selected = null)
        {
            var publishing = await publishongProvider.GetListAsync();
            var selectedItem = selected.HasValue ? publishing.Find(x => x.PublishingCode == selected) : null;
            ViewBag.PublishingID = new SelectList(publishing, "PublishingCode", "Title", selectedItem);
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
