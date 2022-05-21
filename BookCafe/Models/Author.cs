using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorCode { get; set; }
        public string Fio { get; set; }
        public int YearOfBirth { get; set; }
        public string Country { get; set; }
        public int YearOfDeath { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
