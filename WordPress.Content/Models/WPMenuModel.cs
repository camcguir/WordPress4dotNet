using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPress.Content.Models
{
    public class WPMenuModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public Item[] items { get; set; }
        public Meta meta { get; set; }
        public Dictionary<string, string> styles { get; set; }

        public class Meta
        {
            public Links links { get; set; }
        }

        public class Links
        {
            public string collection { get; set; }
            public string self { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public int order { get; set; }
            public int parent { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string attr { get; set; }
            public string target { get; set; }
            public string classes { get; set; }
            public string xfn { get; set; }
            public string description { get; set; }
            public int object_id { get; set; }
            public string _object { get; set; }
            public string object_slug { get; set; }
            public string type { get; set; }
            public string type_label { get; set; }
            public Item[] children { get; set; }
            public Dictionary<string, string> styles { get; set; }
        }


    }
}
