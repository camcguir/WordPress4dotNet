using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordPress.Content.Models;

namespace WordPress.Content.ViewModels
{
    public static class WPContentExtensions
    {
        /// <summary>
        /// Used to safely obtain the contentViewModel title of the specified contentType.  Be sure to note whether the "MENU" contentType is the TopNav or Footer menu. 
        /// </summary>
        /// <param name="contentViewModel"></param>
        /// <param name="contentType"></param>
        /// <param name="footerMenu"></param>
        /// <returns string="title"></returns>
        public static string GetSafeTitle(this WPContentViewModel contentViewModel, WPEnums.ContentTypes contentType, bool footerMenu = false)
        {
            //declarations
            string title = null;

            try
            {
                if (contentViewModel != null)
                {
                    //determine content type and return title
                    switch (contentType)
                    {
                        case WPEnums.ContentTypes.PAGE:
                            title = contentViewModel.PageModel.GetSafeTitle();
                            break;
                        case WPEnums.ContentTypes.POST:
                            title = contentViewModel.PostModel.GetSafeTitle();
                            break;
                        case WPEnums.ContentTypes.POSTCATEGORY:
                            if (contentViewModel.PostCategories != null)
                            {
                                title = contentViewModel.PostCategories.CategoryName;
                            }
                            break;
                        case WPEnums.ContentTypes.MENU:
                            if (footerMenu)
                            {
                                if (contentViewModel.FooterNavMenuModel != null)
                                {
                                    title = contentViewModel.FooterNavMenuModel.name;
                                }
                            }
                            else
                            {
                                if (contentViewModel.TopNavMenuModel != null)
                                {
                                    title = contentViewModel.TopNavMenuModel.name;
                                }
                            }
                            break;
                        case WPEnums.ContentTypes.POSTMEDIA:
                            if (contentViewModel.PostModel.ContainsFeaturedMedia())
                            {
                                var featuredMedia = contentViewModel.PostModel.GetFeaturedMedia();
                                if (featuredMedia?.title != null)
                                {
                                    title = featuredMedia.title.rendered;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                title = "";
            }

            return title;
        }
        
    }
}