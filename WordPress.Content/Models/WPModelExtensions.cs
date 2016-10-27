using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPress.Content.Models
{
    public static class WPModelExtensions
    {
        public static string GetSafeProperty(this Dictionary<string, string> contentItem, string property)
        {
            string returnValue = null;

            if (contentItem != null && contentItem.Any())
            {
                string propertyOut = null;
                contentItem.TryGetValue(property, out propertyOut);
                if (propertyOut != null)
                {
                    returnValue = propertyOut;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Used to safely obtain the POST title. 
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static string GetSafeTitle(this WPPostPageModel contentPostModel)
        {
            string title = "";
            if (contentPostModel?.title != null)
            {
                title = contentPostModel.title.rendered;
            }
            return title;
        }

        /// <summary>
        /// Used to safely obtain the content of the PAGE contentType. 
        /// </summary>
        /// <param name="contentPageModel"></param>
        /// <returns string="content"></returns>
        public static string GetSafeContent(this WPPostPageModel contentPageModel)
        {
            string content = null;
            try
            {
                if (contentPageModel?.content != null)
                {
                    content = contentPageModel.content.rendered;
                }
            }
            catch (Exception ex)
            {
                content = "";
            }

            return content;
        }

        /// <summary>
        /// Used to safely obtain the content of the POST contentType. 
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns string="excerpt"></returns>
        public static string GetSafeExcerpt(this WPPostPageModel contentPostModel)
        {
            string excerpt = null;
            try
            {
                if (contentPostModel?.excerpt != null)
                {
                    excerpt = contentPostModel.excerpt.rendered;
                }
            }
            catch (Exception ex)
            {
                excerpt = "";
            }

            return excerpt;
        }

        /// <summary>
        /// Checks to verify that Content exists 
        /// </summary>
        /// <param name="contentPageModel"></param>
        /// <returns string="content"></returns>
        public static bool ContainsContent(this WPPostPageModel contentPageModel)
        {
            string content = null;
            try
            {
                if (contentPageModel?.content != null)
                {
                    content = contentPageModel.content.rendered;
                }
            }
            catch (Exception ex)
            {
                content = "";
            }

            return !string.IsNullOrEmpty(content);
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsFeaturedMedia(this WPPostPageModel contentPostModel)
        {
            bool wpFeaturedMedia = false;
            try
            {
                if (contentPostModel?._embedded?.wpfeaturedmedia != null &&
                    contentPostModel._embedded.wpfeaturedmedia.Any())
                {
                    wpFeaturedMedia = true;
                }
            }
            catch (Exception ex)
            {
                wpFeaturedMedia = false;
            }

            return wpFeaturedMedia;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsMediaDetails(this WPPostPageModel contentPostModel)
        {
            bool wpMediaDetails = false;
            try
            {
                if (contentPostModel?._embedded?.wpfeaturedmedia != null &&
                    contentPostModel._embedded.wpfeaturedmedia.Any())
                {
                    if (ContainsMediaDetails(contentPostModel._embedded.wpfeaturedmedia))
                    {
                        wpMediaDetails = true;
                    }
                }
            }
            catch (Exception ex)
            {
                wpMediaDetails = false;
            }

            return wpMediaDetails;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details exists
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns></returns>
        public static bool ContainsMediaDetails(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            bool wpMediaDetails = false;
            try
            {
                if (featuredMedia?.First().media_details != null)
                {
                    wpMediaDetails = true;
                }
            }
            catch (Exception ex)
            {
                wpMediaDetails = false;
            }

            return wpMediaDetails;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details.Sizes exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsMediaSizes(this WPPostPageModel contentPostModel)
        {
            bool wpMediaSizes = false;
            try
            {
                if (contentPostModel?._embedded?.wpfeaturedmedia != null &&
                    contentPostModel._embedded.wpfeaturedmedia.Any())
                {
                    if (ContainsMediaSizes(contentPostModel._embedded.wpfeaturedmedia))
                    {
                        wpMediaSizes = true;
                    }
                }
            }
            catch (Exception ex)
            {
                wpMediaSizes = false;
            }

            return wpMediaSizes;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details.Sizes exists
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns></returns>
        public static bool ContainsMediaSizes(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            bool wpMediaSizes = false;
            try
            {
                if (featuredMedia?.First().media_details?.sizes != null)
                {
                    wpMediaSizes = true;
                }
            }
            catch (Exception ex)
            {
                wpMediaSizes = false;
            }

            return wpMediaSizes;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details.Sizes.Full exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsFullSizeImage(this WPPostPageModel contentPostModel)
        {
            bool wpFullSizeImg = false;
            try
            {
                if (contentPostModel?._embedded?.wpfeaturedmedia != null &&
                    contentPostModel._embedded.wpfeaturedmedia.Any())
                {
                    if (ContainsFullSizeImage(contentPostModel._embedded.wpfeaturedmedia))
                    {
                        wpFullSizeImg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                wpFullSizeImg = false;
            }

            return wpFullSizeImg;
        }

        /// <summary>
        /// Checks to verify that FeaturedMedia.Media_Details.Sizes.Full exists
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns></returns>
        public static bool ContainsFullSizeImage(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            bool wpFullSizeImg = false;
            try
            {
                if (featuredMedia?.First().media_details?.sizes?.full != null)
                {
                    wpFullSizeImg = true;
                }
            }
            catch (Exception ex)
            {
                wpFullSizeImg = false;
            }

            return wpFullSizeImg;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns  WPPostModel.WpFeaturedmedia, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns WpFeaturedmedia="empty"></returns>
        public static WPPostPageModel.WpFeaturedmedia GetFeaturedMedia(this WPPostPageModel contentPostModel)
        {
            var wpFeaturedMedia = new WPPostPageModel.WpFeaturedmedia();
            try
            {
                if (ContainsFeaturedMedia(contentPostModel))
                {
                    wpFeaturedMedia = contentPostModel._embedded.wpfeaturedmedia.First();
                }
            }
            catch (Exception ex)
            {

            }

            return wpFeaturedMedia;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns Media_Details="empty"></returns>
        public static WPPostPageModel.Media_Details GetMediaDetails(this WPPostPageModel contentPostModel)
        {
            var wpMediaDetails = new WPPostPageModel.Media_Details();
            try
            {
                if (ContainsMediaDetails(contentPostModel))
                {
                    wpMediaDetails = contentPostModel.GetFeaturedMedia().media_details;
                }
            }
            catch (Exception ex)
            {

            }

            return wpMediaDetails;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns Media_Details="empty"></returns>
        public static WPPostPageModel.Media_Details GetMediaDetails(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            var wpMediaDetails = new WPPostPageModel.Media_Details();
            try
            {
                if (ContainsMediaDetails(featuredMedia))
                {
                    wpMediaDetails = featuredMedia.First().media_details;
                }
            }
            catch (Exception ex)
            {

            }

            return wpMediaDetails;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details.Sizes, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns Sizes="empty"></returns>
        public static WPPostPageModel.Sizes GetMediaSizes(this WPPostPageModel contentPostModel)
        {
            var wpMediaSizes = new WPPostPageModel.Sizes();
            try
            {
                if (ContainsMediaSizes(contentPostModel))
                {
                    wpMediaSizes = contentPostModel.GetMediaDetails().sizes;
                }
            }
            catch (Exception ex)
            {

            }

            return wpMediaSizes;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details.Sizes, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns Sizes="empty"></returns>
        public static WPPostPageModel.Sizes GetMediaSizes(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            var wpMediaSizes = new WPPostPageModel.Sizes();
            try
            {
                if (ContainsMediaSizes(featuredMedia))
                {
                    wpMediaSizes = featuredMedia.GetMediaDetails().sizes;
                }
            }
            catch (Exception ex)
            {

            }

            return wpMediaSizes;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details.Sizes, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns Full="empty"></returns>
        public static WPPostPageModel.Full GetFullSizeImg(this WPPostPageModel contentPostModel)
        {
            var wpFullSizeImg = new WPPostPageModel.Full();
            try
            {
                if (ContainsFullSizeImage(contentPostModel))
                {
                    wpFullSizeImg = contentPostModel.GetMediaSizes().full;
                }
            }
            catch (Exception ex)
            {

            }

            return wpFullSizeImg;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns FeaturedMedia.Media_Details.Sizes, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="featuredMedia"></param>
        /// <returns Full="empty"></returns>
        public static WPPostPageModel.Full GetFullSizeImg(this WPPostPageModel.WpFeaturedmedia[] featuredMedia)
        {
            var wpFullSizeImgs = new WPPostPageModel.Full();
            try
            {
                if (ContainsFullSizeImage(featuredMedia))
                {
                    wpFullSizeImgs = featuredMedia.GetMediaSizes().full;
                }
            }
            catch (Exception ex)
            {

            }

            return wpFullSizeImgs;
        }

        /// <summary>
        /// Checks to verify that WPTerms exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsTerms(this WPPostPageModel contentPostModel)
        {
            bool wpTerms = false;
            try
            {
                if (contentPostModel?._embedded?.wpterm != null && contentPostModel._embedded.wpterm.Any() &&
                    contentPostModel._embedded.wpterm.First().Any())
                {
                    wpTerms = true;
                }
            }
            catch (Exception ex)
            {
                wpTerms = false;
            }

            return wpTerms;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns List of WpTerm1s, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns List="WPTerms"></returns>
        public static List<WPPostPageModel.WpTerm1> GetTerms(this WPPostPageModel contentPostModel)
        {
            var wpTerms = new List<WPPostPageModel.WpTerm1>();
            try
            {
                if (ContainsTerms(contentPostModel))
                {
                    wpTerms = contentPostModel._embedded.wpterm.First().Select(a => a).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return wpTerms;
        }

        /// <summary>
        /// Checks to verify that Author exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsAuthor(this WPPostPageModel contentPostModel)
        {
            bool author = false;
            try
            {
                if (contentPostModel?._embedded?.author != null && contentPostModel._embedded.author.Any())
                {
                    author = true;
                }
            }
            catch (Exception ex)
            {
                author = false;
            }

            return author;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns Author1, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns author="empty"></returns>
        public static WPPostPageModel.Author1 GetAuthor(this WPPostPageModel contentPostModel)
        {
            var wpAuthor = new WPPostPageModel.Author1();
            try
            {
                if (ContainsAuthor(contentPostModel))
                {
                    wpAuthor = contentPostModel._embedded.author.First();
                }
            }
            catch (Exception ex)
            {

            }

            return wpAuthor;
        }

        /// <summary>
        /// Checks to verify that Tags exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsTags(this WPPostPageModel contentPostModel)
        {
            bool tags = false;
            try
            {
                if (contentPostModel?.tags != null && contentPostModel.tags.Any())
                {
                    tags = true;
                }
            }
            catch (Exception ex)
            {
                tags = false;
            }

            return tags;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns Author1, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns ListInt="empty"></returns>
        public static List<int?> GetTags(this WPPostPageModel contentPostModel)
        {
            var tags = new List<int?>();
            try
            {
                if (ContainsTags(contentPostModel))
                {
                    tags = contentPostModel.tags.ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return tags;
        }

        /// <summary>
        /// Checks to verify that TagCollection exists
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns></returns>
        public static bool ContainsTagCollection(this WPPostPageModel contentPostModel)
        {
            bool tags = false;
            try
            {
                if (contentPostModel?.tagColllection != null && contentPostModel.tagColllection.Any())
                {
                    tags = true;
                }
            }
            catch (Exception ex)
            {
                tags = false;
            }

            return tags;
        }

        /// <summary>
        /// NOT A SAFE CHECK.  Method returns Author1, if is exists.  Otherwise, an empty object.
        /// </summary>
        /// <param name="contentPostModel"></param>
        /// <returns tagCollection="empty"></returns>
        public static List<WPPostPageModel.Tag> GetTagCollection(this WPPostPageModel contentPostModel)
        {
            var tags = new List<WPPostPageModel.Tag>();
            try
            {
                if (ContainsTags(contentPostModel))
                {
                    tags = contentPostModel.tagColllection.ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return tags;
        }
    }
}
