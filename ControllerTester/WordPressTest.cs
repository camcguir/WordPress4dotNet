using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPress.Content.Models;
using WordPress.Content.Providers;
using WordPress.Controllers;
using WordPress.Content.ViewModels;

namespace ControllerTester
{
    [TestClass]
    public class WordPressTest
    {
        [TestMethod]
        public void TestMenu()
        {
            var controller = new WordPressController();
            var result = controller.Menu(WordPressController.MenuOptions.TOPNAV);
            Assert.IsTrue(result.items != null && result.items.Any());
        }

        [TestMethod]
        public void TestPageID()
        {
            WPContentViewModel contentViewModel = new WPContentViewModel();
            WPContentProvider _wpContentSvc = new WPContentProvider();
            var id = "INSERT YOUR PAGE ID TO TEST HERE";
            int idNumberValue;

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

            Assert.IsTrue(contentViewModel.PageModel != null && contentViewModel.PageModel.ContainsContent() && !contentViewModel.PageModel.GetSafeContent().Contains("404"));
        }

        [TestMethod]
        public void TestPageSlug()
        {
            WPContentViewModel contentViewModel = new WPContentViewModel();
            WPContentProvider _wpContentSvc = new WPContentProvider();
            var id = "INSERT YOUR SLUG TO TEST HERE";
            int idNumberValue;

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

            Assert.IsTrue(contentViewModel.PageModel != null && contentViewModel.PageModel.ContainsContent() && !contentViewModel.PageModel.GetSafeContent().Contains("404"));
        }

        [TestMethod]
        public void TestPostID()
        {
            WPContentViewModel contentViewModel = new WPContentViewModel();
            WPContentProvider _wpContentSvc = new WPContentProvider();
            var id = "INSERT YOUR POST ID TO TEST HERE";
            int idNumberValue;


            contentViewModel.PostModel =

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                ? (WPPostPageModel)

                    //if the id is an int, apply the "include" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.include, WPEnums.Filters.include.Apply(idNumberValue.ToString())}
                    })
                : (WPPostPageModel)

                    //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(id)}
                    });

            Assert.IsTrue(contentViewModel.PostModel != null && contentViewModel.PostModel.ContainsContent() && !contentViewModel.PostModel.GetSafeContent().Contains("404"));
        }

        [TestMethod]
        public void TestPostSlug()
        {
            WPContentViewModel contentViewModel = new WPContentViewModel();
            WPContentProvider _wpContentSvc = new WPContentProvider();
            var id = "INSERT YOUR SLUG TO TEST HERE";
            int idNumberValue;


            contentViewModel.PostModel =

                //detect whether the id is an int or a string
                Int32.TryParse(id, out idNumberValue)
                ? (WPPostPageModel)

                    //if the id is an int, apply the "include" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.include, WPEnums.Filters.include.Apply(idNumberValue.ToString())}
                    })
                : (WPPostPageModel)

                    //if the id is a string, apply the "slug" filter
                    _wpContentSvc.GetWPContent(WPEnums.ContentTypes.POST,
                    new Dictionary<WPEnums.Filters, string>()
                    {
                        { WPEnums.Filters.slug, WPEnums.Filters.slug.Apply(id)}
                    });

            Assert.IsTrue(contentViewModel.PostModel != null && contentViewModel.PostModel.ContainsContent() && !contentViewModel.PostModel.GetSafeContent().Contains("404"));
        }

        [TestMethod]
        public void TestCategorySlug()
        {
            WPContentViewModel contentViewModel = new WPContentViewModel();
            WPContentProvider _wpContentSvc = new WPContentProvider();
            var id = "INSERT YOUR SLUG TO TEST HERE";
            int idNumberValue;


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

            Assert.IsTrue(contentViewModel.PostCategories != null && contentViewModel.PostCategories.CategoryCollection != null && contentViewModel.PostCategories.CategoryCollection.Any());
        }

    }
}
