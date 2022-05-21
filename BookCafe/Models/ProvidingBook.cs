using System;
using System.Collections.Generic;

#nullable disable

namespace BookShop.Models
{
    public partial class ProvidingBook
    {
        public int ProviderCode { get; set; }
        public int BookCode { get; set; }

        public virtual Book BookCodeNavigation { get; set; }
        public virtual BookProvider ProviderCodeNavigation { get; set; }
    }
}
