using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class DbGenresProvider : IDataProvider<Genre>
    {
        private readonly BookShopContext dbContext;
        public DbGenresProvider(BookShopContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(Genre item)
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Genre item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Genre Get(int id)
        {
            return dbContext.Genres.FirstOrDefault(x => x.GenreCode == id);
        }

        public async Task<List<Genre>> GetListAsync()
        {
            return await dbContext.Genres.ToListAsync();
        }
    }
}
