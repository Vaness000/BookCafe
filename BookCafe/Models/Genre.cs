using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int GenreCode { get; set; }
        public string Title { get; set; }
        public string DescriptionGenre { get; set; }
        public int? DiscountCode { get; set; }

        public virtual Discount DiscountCodeNavigation { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
