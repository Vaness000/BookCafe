using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class OrderClient
    {
        public int OrderCode { get; set; }
        public int ClientCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int? BookCode { get; set; }

        public virtual Book BookCodeNavigation { get; set; }
        public virtual Client ClientCodeNavigation { get; set; }
    }
}
