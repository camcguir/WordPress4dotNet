using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WordPress.Content.Models;

namespace WordPress.Content.Mappers
{
    public class WPModelMapper
    {
        #region Private Variables

        //declarations
        private static string WPEndPoint = ConfigurationManager.AppSettings["WPEndPoint"];

        //constructor
        private static string WPEndPointRegexPattern()
        {
            //declarations
            var directoryPattern = new StringBuilder();

            //split the WordPress origin 
            var directorySplit = WPEndPoint.Split('/');

            //properly decorate the endpoint to be used in the Regex Edit 
            for (int i = 0; i < directorySplit.Count(); i++)
            {
                if (i == 0)
                {
                    //do not append "/" to the URL protocol
                    directoryPattern.Append(directorySplit[i]);
                }
                else
                {
                    //append "/" to each directory notation
                    directoryPattern.Append(@"\\\/" + directorySplit[i]);
                }
            }

            //cast the string builder to string and prepare for use
            return directoryPattern.ToString().Replace(".", @"\.");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Map properties from the Json response to models.  Reusable function for all WP ContentTypes
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="contentType"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public static object MapWordPressProperties(dynamic jsonObj, WPEnums.ContentTypes contentType, Dictionary<WPEnums.Filters, string> filterParams)
        {
            var jsonString = ReplaceNativeJsonProperties(jsonObj);
            if (jsonString != null)
            {
                switch (contentType)
                {
                    //WP API V2 Plugin Required
                    case WPEnums.ContentTypes.PAGE:
                        return MapWPPage(jsonString);
                        break;
                    case WPEnums.ContentTypes.POST:
                        return MapWPPost(jsonString);
                        break;
                    case WPEnums.ContentTypes.CATEGORY:
                        return MapWPCategory(jsonString);
                        break;
                    case WPEnums.ContentTypes.POSTCATEGORY:
                        return MapWPPostCategory(jsonString, filterParams);
                        break;
                    case WPEnums.ContentTypes.TAG:
                        return MapWPTags(jsonString);
                        break;
                    ////WP API V2 MENU Plugin Required
                    case WPEnums.ContentTypes.MENU:
                        return MapWPMenu(jsonString);
                        break;
                    case WPEnums.ContentTypes.MENUCOLLECTION:
                        return MapWPMenuCollection(jsonString);
                        break;
                    default:
                        return new WPPostPageModel();
                        break;
                }
            }

            return new WPPostPageModel();
        }

        #endregion

        #region Private Methods
        private static WPPostPageModel MapWPPage(dynamic jsonObj)
        {
            //object declaration
            var wpPageModel = new WPPostPageModel();
            var wpPageModelList = new List<WPPostPageModel>();

            try
            {
                //for a collection PAGE content request, but only returns a single page
                wpPageModelList = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(List<WPPostPageModel>));
                if (wpPageModelList != null && wpPageModelList.Any())
                {
                    wpPageModel = wpPageModelList.First();
                }
            }
            catch (Exception ex)
            {
                //for a single PAGE content request
                wpPageModel = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(WPPostPageModel));
            }

            //ensuring that app dependent values are set in the page model before returning
            if (wpPageModelList != null && wpPageModelList.Any())
            {
                wpPageModelList.ForEach(EnsurePostPageValues);
            }
            else
            {
                EnsurePostPageValues(wpPageModel);
            }

            return wpPageModel;
        }

        private static WPPostPageModel MapWPPost(dynamic jsonObj)
        {
            //object declaration
            var wpPostCollectionModel = new WPPostPageModel();
            var wpPostCollectionModelList = new List<WPPostPageModel>();

            try
            {
                //for a collection POST content request, but only returns a single post
                wpPostCollectionModelList = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(List<WPPostPageModel>));
                if (wpPostCollectionModelList != null && wpPostCollectionModelList.Any())
                {
                    wpPostCollectionModel = wpPostCollectionModelList.First();
                }
            }
            catch (Exception ex)
            {
                //for a single POST content request
                wpPostCollectionModel = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(WPPostPageModel));
            }

            //ensuring that app dependent values are set in the page model before returning
            if (wpPostCollectionModelList != null && wpPostCollectionModelList.Any())
            {
                wpPostCollectionModelList.ForEach(EnsurePostPageValues);
            }
            else
            {
                EnsurePostPageValues(wpPostCollectionModel);
            }

            return wpPostCollectionModel;
        }

        private static WPCategoryModel MapWPCategory(dynamic jsonObj)
        {
            //object declaration
            var wpCategoryModel = new WPCategoryModel();
            var wpCategoryModelList = new List<WPCategoryModel>();

            try
            {
                //for a single CATEGORY content request
                wpCategoryModel = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof (WPCategoryModel));
            }
            catch (Exception ex)
            {
                //for a collection CATEGORY content request, but only returns a single category
                wpCategoryModelList = JsonConvert.DeserializeObject(jsonObj.ToString(), typeof (List<WPCategoryModel>));
                if (wpCategoryModelList != null && wpCategoryModelList.Any())
                {
                    wpCategoryModel = wpCategoryModelList.First();
                }
            }

            return wpCategoryModel;
        }

        private static WPPostCategories MapWPPostCategory(dynamic jsonObj, Dictionary<WPEnums.Filters, string> filterParams)
        {
            //object declaration
            var wpPostCategoriesModel = new WPPostCategories();

            //object serialization and ensuring values
            wpPostCategoriesModel.CategoryCollection = JsonConvert.DeserializeObject(jsonObj, typeof (List<WPPostPageModel>));
            wpPostCategoriesModel.CategoryCollection.ForEach(EnsurePostPageValues);

            //assign the view model category name 
            if (filterParams != null && filterParams.Count > 0)
            {
                if (filterParams.ContainsKey(WPEnums.Filters.filter) &&
                    filterParams[WPEnums.Filters.filter].Contains("category_name"))
                {
                    var categoryParam = filterParams[WPEnums.Filters.filter].Split('=');
                    if (categoryParam != null && categoryParam.Any())
                    {
                        //retrive the category slug
                        var categorySlug = categoryParam.Last();
                        if (wpPostCategoriesModel.CategoryCollection.Any())
                        {
                            try
                            {
                                //find the first category item where the slug matches the category_name filter, and grab the category name
                                var name =
                                    wpPostCategoriesModel.CategoryCollection.Select(
                                        a =>
                                            a._embedded?.wpterm?.First()
                                                .Where(b => b.slug == categorySlug)
                                                .Select(c => c.name)
                                                .First());
                                wpPostCategoriesModel.CategoryName = name.Any() ? name.First() : categorySlug.ToUpper();
                            }
                            catch (Exception ex)
                            {
                                wpPostCategoriesModel.CategoryName = categorySlug.ToUpper();
                            }
                        }
                    }
                }

            }

            return wpPostCategoriesModel;

        }

        private static List<WPPostPageModel.Tag> MapWPTags(dynamic jsonObj)
        {
            var wpPostTagModel = new List<WPPostPageModel.Tag>();
            try
            {
                //for a collection TAG content request
                List<WPTagModel> wpTagModel = JsonConvert.DeserializeObject(jsonObj, typeof(List<WPTagModel>));
                wpTagModel?.ForEach(tag =>
                {
                    var tagItem = new WPPostPageModel.Tag()
                    {
                        id = tag.id,
                        name = tag.name,
                        slug = tag.slug
                    };

                    wpPostTagModel.Add(tagItem);
                });
            }
            catch (Exception ex)
            {
                
            }

            return wpPostTagModel;

        }

        private static WPMenuModel MapWPMenu(dynamic jsonObj)
        {
            var wpMenuModel = new WPMenuModel();

            //for a single MENU content request
            wpMenuModel = JsonConvert.DeserializeObject(jsonObj, typeof (WPMenuModel));

            EnsureMenuSlugValue(wpMenuModel);

            WPMenuBuilder.AddMenuStyle(wpMenuModel);

            return wpMenuModel;
        }

        private static List<WPMenuModel> MapWPMenuCollection(dynamic jsonObj)
        {
            var wpMenuModelCollection = new List<WPMenuModel>();

            //for a collection MENU content request
            wpMenuModelCollection = JsonConvert.DeserializeObject(jsonObj, typeof(List<WPMenuModel>));

            foreach (var wpMenuModel in wpMenuModelCollection)
            {
                EnsureMenuSlugValue(wpMenuModel);
                WPMenuBuilder.AddMenuStyle(wpMenuModel);
            }
            
            return wpMenuModelCollection;
        }

        /// <summary>
        /// Method is used to ensure that newly declared Post/Page objects have necessary property values when returned
        /// </summary>
        /// <param name="page"></param>
        private static void EnsurePostPageValues(WPPostPageModel page)
        {
            page.slug = page.slug ?? page.id.ToString();
            page.type = page.type ?? "post";

            //Place default values into the Post/Page Model to ensure a successful delivery
            if (page.title == null)
            {
                page.title = new WPPostPageModel.Title();
                page.title.rendered = @"404 - Not Found";
            }

            if (page.content == null)
            {
                page.content = new WPPostPageModel.Content();
                page.content.rendered = @"<h2>404 - Not Found</h2>";
            }
        }

        /// <summary>
        /// Method is used to ensure that menu values that will be used for navigation are properply populated
        /// </summary>
        /// <param name="wpMenuModel"></param>
        private static void EnsureMenuSlugValue(WPMenuModel wpMenuModel)
        {
            //loop through each menu item and apply
            if (wpMenuModel != null && wpMenuModel.items != null && wpMenuModel.items.Any())
            {
                foreach (var wpItem in wpMenuModel.items)
                {
                    EnsureMenuSlugValue(wpItem);
                }
            }
        }

        private static void EnsureMenuSlugValue(WPMenuModel.Item wpItem)
        {
            //declaration
            var slugValue = wpItem.object_slug;
            var typeValue = wpItem.type;
            var url = wpItem.url;
            var id = wpItem.object_id.ToString();


            //special treatment for taxonomy style types
            if (typeValue.ToString() == "taxonomy")
            {
                if (url != null)
                {
                    //grab WordPress origin regex pattern
                    var wpOriginPattern = WPEndPointRegexPattern();

                    //Replace link references to WordPress origin
                    string classifiedPattern = string.Format(@"(?<={0}\/category)*", wpOriginPattern);
                    string classifiedReplacement = WPEndPoint + @"/category";
                    Regex classifiedRegex = new Regex(classifiedPattern);
                    if (classifiedRegex.IsMatch(url))
                    {
                        slugValue = url.Replace(classifiedReplacement, "");
                    }

                }
            }
            else
            {
                //as a last ditch effort, assign the id as the slug
                if (string.IsNullOrEmpty(slugValue))
                {
                    if (id != null)
                    {
                        slugValue = id.ToString();
                    }
                }

            }

            //replace the original slug value with the slug value from the URL
            wpItem.object_slug = slugValue;

            //loop through each menu child item and ensure the same conditions apply
            if (wpItem.children != null && wpItem.children.Any())
            {
                foreach (var childItem in wpItem.children)
                {
                    EnsureMenuSlugValue(childItem);
                }

            }

        }

        /// <summary>
        /// This method is used to reformat property names or replace content elements to simplify the deserialization process
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        private static string ReplaceNativeJsonProperties(dynamic jsonObj)
        {
            //convert obj to string
            string jsonString = jsonObj.ToString();

            //grab WordPress origin regex pattern
            var wpOriginPattern = WPEndPointRegexPattern();


            //Replace link references to the WordPress Origin that contain WP native routes
            string classifiedPattern = string.Format(@"href=\\(.|\s){0}(?=\\\/category|\\\/author|\\\/post|\\\/page)",wpOriginPattern);
            string classifiedReplacement = @"href=\""";
            Regex classifiedRegex = new Regex(classifiedPattern);
            jsonString = classifiedRegex.Replace(jsonString, classifiedReplacement);

            //Replace link references to the WordPress Origin that DO NOT contain WP native routes
            string unClassifiedPattern = string.Format(@"href=\\(.|\s){0}(?!\\\/category|\\\/author|\\\/post|\\\/page|\\\/wp-content)", wpOriginPattern);
            string unClassifiedReplacement = @"href=\""\/content";
            Regex unClassifiedRegex = new Regex(unClassifiedPattern);
            jsonString = unClassifiedRegex.Replace(jsonString, unClassifiedReplacement);

            return jsonString;
        }
    }
    #endregion
}
