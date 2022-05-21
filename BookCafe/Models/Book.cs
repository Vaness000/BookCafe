using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Book
    {
        public Book()
        {
            DonatedBooks = new HashSet<DonatedBook>();
            OrderClients = new HashSet<OrderClient>();
            ProvidingBooks = new HashSet<ProvidingBook>();
        }

        public int BookCode { get; set; }
        public int AuthorCode { get; set; }
        public int GenreCode { get; set; }
        public int PublishingCode { get; set; }
        public int YearOfWriting { get; set; }
        public int YearOfPublishing { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int? Img { get; set; }

        public virtual Author AuthorCodeNavigation { get; set; }
        public virtual Genre GenreCodeNavigation { get; set; }
        public virtual BooksImage ImgNavigation { get; set; }
        public virtual Publishing PublishingCodeNavigation { get; set; }
        public virtual ICollection<DonatedBook> DonatedBooks { get; set; }
        public virtual ICollection<OrderClient> OrderClients { get; set; }
        public virtual ICollection<ProvidingBook> ProvidingBooks { get; set; }
    }
}
