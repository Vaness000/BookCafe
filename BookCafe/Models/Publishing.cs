using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Publishing
    {
        public Publishing()
        {
            Books = new HashSet<Book>();
        }

        public int PublishingCode { get; set; }
        public string Title { get; set; }
        public string City { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
