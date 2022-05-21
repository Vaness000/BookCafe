using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class Client
    {
        public Client()
        {
            DonatedBooks = new HashSet<DonatedBook>();
            OrderClients = new HashSet<OrderClient>();
        }

        public int ClientCode { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? DiscountCode { get; set; }

        public virtual Discount DiscountCodeNavigation { get; set; }
        public virtual ICollection<DonatedBook> DonatedBooks { get; set; }
        public virtual ICollection<OrderClient> OrderClients { get; set; }
    }
}
