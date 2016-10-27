using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPress.Content.Models
{
    public class WPTagModel
    {
        public int id { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string taxonomy { get; set; }
        public _Links _links { get; set; }


        public class _Links
        {
            public Self[] self { get; set; }
            public Collection[] collection { get; set; }
            public About[] about { get; set; }
            public WpPost_Type[] wppost_type { get; set; }
            public Cury[] curies { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Collection
        {
            public string href { get; set; }
        }

        public class About
        {
            public string href { get; set; }
        }

        public class WpPost_Type
        {
            public string href { get; set; }
        }

        public class Cury
        {
            public string name { get; set; }
            public string href { get; set; }
            public bool templated { get; set; }
        }

    }
}
