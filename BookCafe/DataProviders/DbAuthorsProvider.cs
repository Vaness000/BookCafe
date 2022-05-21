using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class DbAuthorsProvider : IDataProvider<Author>
    {
        private readonly BookShopContext dbContext;
        public DbAuthorsProvider(BookShopContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(Author item)
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Author item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Author Get(int id)
        {
            return dbContext.Authors.FirstOrDefault(x => x.AuthorCode == id);
        }

        public async Task<List<Author>> GetListAsync()
        {
            return await dbContext.Authors.ToListAsync();
        }
    }
}
