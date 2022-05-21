using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public class DbPublishingProvider : IDataProvider<Publishing>
    {
        private readonly BookShopContext dbContext;
        public DbPublishingProvider(BookShopContext context)
        {
            this.dbContext = context;
        }
        public async Task CreateAsync(Publishing item)
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Publishing item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Publishing Get(int id)
        {
            return dbContext.Publishings.FirstOrDefault(x => x.PublishingCode == id);
        }

        public async Task<List<Publishing>> GetListAsync()
        {
            return await dbContext.Publishings.ToListAsync();
        }
    }
}
