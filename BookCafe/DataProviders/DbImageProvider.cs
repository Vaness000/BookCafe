using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class DbImageProvider : IDataProvider<BooksImage>
    {
        private readonly BookShopContext dbContext;
        public DbImageProvider(BookShopContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(BooksImage item)
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(BooksImage item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public BooksImage Get(int id)
        {
            return dbContext.BooksImages.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<BooksImage>> GetListAsync()
        {
            return await dbContext.BooksImages.ToListAsync();
        }
    }
}
