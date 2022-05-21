using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Breadcrumb
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(Action) || Action == "Index" ? Controller : Action;
            }
        }

        public Breadcrumb(string controller, string action)
        {
            this.Controller = controller;
            this.Action = action;
        }
    }
}
