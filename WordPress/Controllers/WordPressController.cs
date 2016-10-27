using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordPress.Content.Models;
using WordPress.Content.Providers;
using WordPress.Content.ViewModels;

namespace WordPress.Controllers
{
    public class WordPressController : Controller
    {
        #region Declaraions
        //declarations
        private static WPContentProvider _wpContentSvc = new WPContentProvider();
        private static int idNumberValue;
        private static string TopNavMenuId = ConfigurationManager.AppSettings["TopNavMenuID"];
        private static string FootNavMenuId = ConfigurationManager.AppSettings["FootNavMenuID"];
        public enum MenuOptions
        {
            TOPNAV,
            FOOTNAV
        }
        #endregion

        #region Constructors
        //contructors
        private WPContentViewModel contentViewModel = new WPContentViewModel()
        {
            TopNavMenuModel = GetMenuContentCollection(TopNavMenuId),
            FooterNavMenuModel = GetMenuContentCollection(FootNavMenuId)
        };
        #endregion

        #region Public Methods
        // GET: WordPress

        /// <summary>
        /// Method is used as a default for retrieving the homepage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            contentViewModel.PageModel = 

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                ? (WPPostPageModel)

                //if the id is an int, apply the "include" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.PAGE, new Dictionary<WPEnums.Filters, string>()
                    {
                        {WPEnums.Filters.include, WPEnums.Filters.include.Apply(idNumberValue.ToString())}
                    })
                : (WPPostPageModel)

                //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.PAGE,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        {WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(id)}
                    });

            //set the viewBag title for the home page (which is the only page that uses the Index action)
            ViewBag.Title = "WordPress 4 .NET";

            return View("~/Views/Content/WPPage.cshtml", contentViewModel);
        }

        /// <summary>
        /// Method is used for retrieving Page content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Page(string id)
        {
            contentViewModel.PageModel =

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                ? (WPPostPageModel)

                    //if the id is an int, apply the "include" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.PAGE, new Dictionary<WPEnums.Filters, string>()
                    {
                        {WPEnums.Filters.include, WPEnums.Filters.include.Apply(idNumberValue.ToString())}
                    })
                : (WPPostPageModel)

                    //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.PAGE,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        {WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(id)}
                    });

            //set the viewBag title for the home page, otherwise, the viewBag title is set on the View 
            if (id.Contains("home"))
            {
                ViewBag.Title = "WordPress 4 .NET";
            }

            return View("~/Views/Content/WPPage.cshtml", contentViewModel);
        }

        /// <summary>
        /// Method is used for retreiving Post content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Post(string id)
        {
            contentViewModel.PostModel =

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                ? (WPPostPageModel)

                    //if the id is an int, apply the "include" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.include, WPEnums.Filters.include.Apply(idNumberValue.ToString())},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                    })
                : (WPPostPageModel)

                    //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(id)},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                    });

            //the viewBag title can be set on the view
            return View("~/Views/Content/WPPost.cshtml", contentViewModel);
        }

        /// <summary>
        /// Method is used when other controllers want to inherit from WPController and use the Menu capabilities
        /// </summary>
        /// <param name="menuOption"></param>
        /// <returns></returns>
        public WPMenuModel Menu(MenuOptions menuOption)
        {
            var contentModel = new WPMenuModel();

            switch (menuOption)
            {
                case MenuOptions.TOPNAV:
                    contentModel = GetMenuContentCollection(TopNavMenuId);
                    break;
                case MenuOptions.FOOTNAV:
                    contentModel = GetMenuContentCollection(FootNavMenuId);
                    break;
                default:
                    contentModel = GetMenuContentCollection(TopNavMenuId);
                    break;
            }

            return contentModel;
        }

        /// <summary>
        /// Method is used for retrieving a collection of posts under the category nomination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Category(string id)
        {
            contentViewModel.PostCategories =

                //detect whether the id is an int or a string
                //both implementation use the "_embed" filter, which is required for featuredMedia, wpterms, authors
                Int32.TryParse(id, out idNumberValue)
                   ? (WPPostCategories)

                       //if the id is an int, apply the "categories" filter
                       _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POSTCATEGORY,
                       new Dictionary<WPEnums.Filters, string>()
                       {
                        { WPEnums.Filters.categories, WPEnums.Filters.categories.Apply(idNumberValue.ToString())},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                       })
                   : (WPPostCategories)

                       //if the id is a string, apply the "filter" params
                       _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POSTCATEGORY,
                       new Dictionary<WPEnums.Filters, string>()
                       {
                        { WPEnums.Filters.filter, WPEnums.Filters.filter.Apply(id,"category_name")},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                       });

            //the viewBag title can be set on the view
            return View("~/Views/Content/WPCategory.cshtml", contentViewModel);
        }

        /// <summary>
        /// Method is used for retrieving a collection of posts under the author nomination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Author(string id)
        {
            contentViewModel.PostCategories =

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                   ? (WPPostCategories)

                       //if the id is an int, apply the "categories" filter
                       _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                       new Dictionary<WPEnums.Filters, string>()
                       {
                        { WPEnums.Filters.categories, WPEnums.Filters.categories.Apply(idNumberValue.ToString())},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                       })
                   : (WPPostCategories)

                       //if the id is a string, apply the "filter" params
                       _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POSTCATEGORY,
                       new Dictionary<WPEnums.Filters, string>()
                       {
                        { WPEnums.Filters.filter, WPEnums.Filters.filter.Apply(id,"author")},
                        { WPEnums.Filters._embed, WPEnums.Filters._embed.Apply("_embed")}
                       });

            //the viewBag title can be set on the view
            return View("~/Views/Content/WPCategory.cshtml", contentViewModel);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method is used for retrieving a collection of menu items and their styles
        /// </summary>
        /// <param name="menuIdentifier"></param>
        /// <returns></returns>
        private static WPMenuModel GetMenuContentCollection(string menuIdentifier)
        {
            var menuID = menuIdentifier;
            var menuModel =

                //detect whether the id is an int or a string
                Int32.TryParse(menuID, out idNumberValue)
                ? (WPMenuModel)

                    //if the id is an int, apply the "menuID" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.MENU,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.menuID, WPEnums.Filters.menuID.Apply(idNumberValue.ToString())}
                    })
                : (WPMenuModel)

                    //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.MENU,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(menuID)}
                    });

            return menuModel;
        }
        #endregion
    }
}