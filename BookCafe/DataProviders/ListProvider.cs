using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class ListProvider : IDataProvider<Book>
    {
        private List<Book> books;
        public ListProvider()
        {
            books = new List<Book>()
            {
                new Book()
                {
                    BookCode = 1,
                    AuthorCode = 1,
                    GenreCode = 1,
                    PublishingCode = 1,
                    YearOfWriting = 1999,
                    YearOfPublishing = 2000,
                    Title = "Poem",
                    Price = 100
                },
            
                new Book()
                {
                    BookCode = 2,
                    AuthorCode = 2,
                    GenreCode = 2,
                    PublishingCode = 2,
                    YearOfWriting = 2000,
                    YearOfPublishing = 2001,
                    Title = "Book2",
                    Price = 200
                }
            };
        }
        public Task CreateAsync(Book item)
        {

            return Task.Run(() => books.Add(item));
        }

        public Task EditAsync(Book item)
        {
            return Task.Run(() =>{
                Book bookToUpdate = books.FirstOrDefault(x => x.BookCode == item.BookCode);
                if (bookToUpdate != null)
                {
                    bookToUpdate.GenreCode = item.GenreCode;
                    bookToUpdate.AuthorCode = item.AuthorCode;
                    bookToUpdate.Price = item.Price;
                    bookToUpdate.PublishingCode = item.PublishingCode;
                    bookToUpdate.Title = item.Title;
                    bookToUpdate.YearOfPublishing = item.YearOfPublishing;
                    bookToUpdate.YearOfWriting = item.YearOfWriting;
                }
            });
        }

        public Book Get(int id)
        {
            return books.FirstOrDefault(x => x.BookCode == id);
        }

        public Task<List<Book>> GetListAsync()
        {
            return Task.Run(() => { 
                List<Book> bookList = new List<Book>();
                bookList.AddRange(books);
                return bookList;
            });
        }
    }
}
