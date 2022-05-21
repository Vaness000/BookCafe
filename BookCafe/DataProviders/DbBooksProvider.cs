using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class DbBooksProvider : IDataProvider<Book>
    {
        private readonly BookShopContext dbContext;
        public DbBooksProvider(BookShopContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(Book item)
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Book item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Book Get(int id)
        {
            return dbContext.Books.FirstOrDefault(x => x.BookCode == id);
        }

        public async Task<List<Book>> GetListAsync()
        {
            return await dbContext.Books.Include(x => x.AuthorCodeNavigation)
                                     .Include(x => x.GenreCodeNavigation)
                                     .Include(x => x.PublishingCodeNavigation)
                                     .Include(x => x.ImgNavigation).ToListAsync();
        }
    }
}
