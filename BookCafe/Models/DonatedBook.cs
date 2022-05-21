using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class DonatedBook
    {
        public int ClientCode { get; set; }
        public int BookCode { get; set; }

        public virtual Book BookCodeNavigation { get; set; }
        public virtual Client ClientCodeNavigation { get; set; }
    }
}
