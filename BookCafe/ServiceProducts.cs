using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop
{
    public class ServiceProducts
    {
        public int Amount { get; private set; }
        public ServiceProducts(int amount)
        {
            Amount = amount;
        }

        public ServiceProducts()
        {
        }
    }
}
