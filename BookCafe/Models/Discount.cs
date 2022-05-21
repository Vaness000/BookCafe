using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Clients = new HashSet<Client>();
            Genres = new HashSet<Genre>();
        }

        public int DiscountCode { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
