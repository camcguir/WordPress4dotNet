using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordPress.Content.Models
{
    public class WPPostPageModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime date_gmt { get; set; }
        public Guid guid { get; set; }
        public DateTime modified { get; set; }
        public DateTime modified_gmt { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public Title title { get; set; }
        public Content content { get; set; }
        public Excerpt excerpt { get; set; }
        public int author { get; set; }
        public int featured_media { get; set; }
        public string comment_status { get; set; }
        public string ping_status { get; set; }
        public bool sticky { get; set; }
        public string format { get; set; }
        public int[] categories { get; set; }
        public int?[] tags { get; set; }
        public List<Tag> tagColllection { get; set; }
        public _Links _links { get; set; }
        public _Embedded _embedded { get; set; }

        //page properties
        public int parent { get; set; }
        public int menu_order { get; set; }
        public string template { get; set; }


        public class Guid
        {
            public string rendered { get; set; }
        }

        public class Title
        {
            public string rendered { get; set; }
        }

        public class Tag
        {
            public int id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }

        }

        public class Content
        {
            public string rendered { get; set; }
            public bool _protected { get; set; }
        }

        public class Excerpt
        {
            public string rendered { get; set; }
            public bool _protected { get; set; }
        }

        public class _Links
        {
            public Self[] self { get; set; }
            public Collection[] collection { get; set; }
            public About[] about { get; set; }
            public Author[] author { get; set; }
            public Reply[] replies { get; set; }
            public VersionHistory[] versionhistory { get; set; }
            public WpAttachment[] wpattachment { get; set; }
            public WpTerm[] wpterm { get; set; }
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

        public class Author
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class Reply
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class VersionHistory
        {
            public string href { get; set; }
        }

        //public class WpFeaturedmedia
        //{
        //    public bool embeddable { get; set; }
        //    public string href { get; set; }
        //}

        public class WpAttachment
        {
            public string href { get; set; }
        }

        public class WpTerm
        {
            public string taxonomy { get; set; }
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class Cury
        {
            public string name { get; set; }
            public string href { get; set; }
            public bool templated { get; set; }
        }

        public class _Embedded
        {
            public Author1[] author { get; set; }
            public Reply1[][] replies { get; set; }

            [JsonProperty("wp:featuredmedia")]
            public WpFeaturedmedia[] wpfeaturedmedia { get; set; }

            [JsonProperty("wp:term")]
            public WpTerm1[][] wpterm { get; set; }
        }

        public class Author1
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string description { get; set; }
            public string link { get; set; }
            public string slug { get; set; }
            public Avatar_Urls avatar_urls { get; set; }
            public _Links1 _links { get; set; }
        }

        public class _Links1
        {
            public Self1[] self { get; set; }
            public Collection1[] collection { get; set; }
        }

        public class Self1
        {
            public string href { get; set; }
        }

        public class Collection1
        {
            public string href { get; set; }
        }

        public class Reply1
        {
            public int id { get; set; }
            public int parent { get; set; }
            public int author { get; set; }
            public string author_name { get; set; }
            public string author_url { get; set; }
            public DateTime date { get; set; }
            public Content1 content { get; set; }
            public string link { get; set; }
            public string type { get; set; }
            public _Links2 _links { get; set; }
        }

        public class Content1
        {
            public string rendered { get; set; }
        }

        public class _Links2
        {
            public Self2[] self { get; set; }
            public Collection2[] collection { get; set; }
            public Up[] up { get; set; }
        }

        public class Self2
        {
            public string href { get; set; }
        }

        public class Collection2
        {
            public string href { get; set; }
        }

        public class Up
        {
            public bool embeddable { get; set; }
            public string post_type { get; set; }
            public string href { get; set; }
        }

        
        public class WpFeaturedmedia
        {
            public int id { get; set; }
            public DateTime date { get; set; }
            public string slug { get; set; }
            public string type { get; set; }
            public string link { get; set; }
            public Title1 title { get; set; }
            public int author { get; set; }
            public string alt_text { get; set; }
            public string media_type { get; set; }
            public string mime_type { get; set; }
            public Media_Details media_details { get; set; }
            public string source_url { get; set; }
            public _Links3 _links { get; set; }
        }

        public class Title1
        {
            public string rendered { get; set; }
        }

        public class Media_Details
        {
            public int width { get; set; }
            public int height { get; set; }
            public string file { get; set; }
            public Sizes sizes { get; set; }
            public Image_Meta image_meta { get; set; }
        }

        public class Sizes
        {
            public Thumbnail thumbnail { get; set; }
            public Medium medium { get; set; }
            public Large large { get; set; }

            [JsonProperty("blog-large")]
            public BlogLarge bloglarge { get; set; }

            [JsonProperty("blog-medium")]
            public BlogMedium blogmedium { get; set; }

            [JsonProperty("portfolio-full")]
            public PortfolioFull portfoliofull { get; set; }

            [JsonProperty("portfolio-one")]
            public PortfolioOne portfolioone { get; set; }

            [JsonProperty("portfolio-two")]
            public PortfolioTwo portfoliotwo { get; set; }

            [JsonProperty("portfolio-three")]
            public PortfolioThree portfoliothree { get; set; }

            [JsonProperty("portfolio-five")]
            public PortfolioFive portfoliofive { get; set; }
            public RecentPosts recentposts { get; set; }

            [JsonProperty("recent-works-thumbnail")]
            public RecentWorksThumbnail recentworksthumbnail { get; set; }
            public _200 _200 { get; set; }
            public _400 _400 { get; set; }
            public _600 _600 { get; set; }
            public _800 _800 { get; set; }
            public _1200 _1200 { get; set; }
            public Full full { get; set; }
        }

        public class Thumbnail
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class Medium
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class Large
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class BlogLarge
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class BlogMedium
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class PortfolioFull
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class PortfolioOne
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class PortfolioTwo
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class PortfolioThree
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class PortfolioFive
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class RecentPosts
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class RecentWorksThumbnail
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class _200
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class _400
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class _600
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class _800
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class _1200
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class Full
        {
            public string file { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string mime_type { get; set; }
            public string source_url { get; set; }
        }

        public class Image_Meta
        {
            public string aperture { get; set; }
            public string credit { get; set; }
            public string camera { get; set; }
            public string caption { get; set; }
            public string created_timestamp { get; set; }
            public string copyright { get; set; }
            public string focal_length { get; set; }
            public string iso { get; set; }
            public string shutter_speed { get; set; }
            public string title { get; set; }
            public string orientation { get; set; }
            public object[] keywords { get; set; }
            public string[] resized_images { get; set; }
        }

        public class _Links3
        {
            public Self3[] self { get; set; }
            public Collection3[] collection { get; set; }
            public About1[] about { get; set; }
            public Author2[] author { get; set; }
            public Reply2[] replies { get; set; }
        }

        public class Self3
        {
            public string href { get; set; }
        }

        public class Collection3
        {
            public string href { get; set; }
        }

        public class About1
        {
            public string href { get; set; }
        }

        public class Author2
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class Reply2
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class WpTerm1
        {
            public int id { get; set; }
            public string link { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string taxonomy { get; set; }
            public _Links4 _links { get; set; }
        }

        public class _Links4
        {
            public Self4[] self { get; set; }
            public Collection4[] collection { get; set; }
            public About2[] about { get; set; }
            public Up1[] up { get; set; }

            [JsonProperty("wp:post_type")]
            public WpPost_Type[] wppost_type { get; set; }
            public Cury1[] curies { get; set; }
        }

        public class Self4
        {
            public string href { get; set; }
        }

        public class Collection4
        {
            public string href { get; set; }
        }

        public class About2
        {
            public string href { get; set; }
        }

        public class Up1
        {
            public bool embeddable { get; set; }
            public string href { get; set; }
        }

        public class WpPost_Type
        {
            public string href { get; set; }
        }

        public class Cury1
        {
            public string name { get; set; }
            public string href { get; set; }
            public bool templated { get; set; }
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        
        public class Avatar_Urls
        {
            public string _24 { get; set; }
            public string _48 { get; set; }
            public string _96 { get; set; }
        }
    }
}
