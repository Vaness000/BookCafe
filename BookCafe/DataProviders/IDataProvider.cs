using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataProviders
{
    public interface IDataProvider<T>
    {
        Task<List<T>> GetListAsync();
        T Get(int id);
        Task CreateAsync(T item);
        Task EditAsync(T item);
    }
}
