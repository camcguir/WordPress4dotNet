using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPress.Content.Models
{
    public class WPPostCategories
    {
        public string CategoryName { get; set; }
        public List<WPPostPageModel> CategoryCollection { get; set; }
    }
}
