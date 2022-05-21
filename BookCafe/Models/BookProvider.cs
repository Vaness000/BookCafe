using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class BookProvider
    {
        public BookProvider()
        {
            ProvidingBooks = new HashSet<ProvidingBook>();
        }

        public int ProviderCode { get; set; }
        public string Title { get; set; }
        public string City { get; set; }

        public virtual ICollection<ProvidingBook> ProvidingBooks { get; set; }
    }
}
