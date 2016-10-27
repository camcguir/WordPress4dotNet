using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordPress.Content.Models;

namespace WordPress.Content.ViewModels
{
    public class WPContentViewModel
    {
        public WPMenuModel TopNavMenuModel { get; set; }
        public WPMenuModel FooterNavMenuModel { get; set; }
        public WPPostPageModel PageModel { get; set; }
        public WPPostPageModel PostModel { get; set; }
        public WPPostCategories PostCategories { get; set; }

    }
}