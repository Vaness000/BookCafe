using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly Models.BookShopContext context;

        public WelcomeController(Models.BookShopContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await context.Genres.ToListAsync());
        }
    }
}
