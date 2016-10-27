using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WordPress.Content.Helpers;
using WordPress.Content.Mappers;
using WordPress.Content.Models;

namespace WordPress.Content.Providers
{
    public class WPContentProvider
    {
        #region Public Variables

        //declarations
        public MemoryCache _memoryCache = MemoryCache.Default;

        #endregion

        #region Private Variables

        private string WPEndPoint = ConfigurationManager.AppSettings["WPEndPoint"];
        private string MockRequest = ConfigurationManager.AppSettings["MockRequest"];

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to acquire and map various WordPress content items
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="filterParams"></param>
        /// <param name="genericGet"></param>
        /// <returns></returns>
        public object GetWPContent(WPEnums.ContentTypes contentType, Dictionary<WPEnums.Filters, string> filterParams)
        {
            //retrive the end point for calling WP
            var endPoint = GetEndPointPath(contentType);

            //append filters to the WP REST endpoing
            endPoint = BuildLinkWithFilters(contentType, filterParams, endPoint);

            //create the cachekey for caching the mapped response
            var cacheKey = BuildCacheKey(contentType, endPoint);

            //Call for WP content, Map the response, and Cache the mapped response
            var mappedWPContent = CacheAndMapWpContent(contentType, filterParams, endPoint, cacheKey);

            return mappedWPContent;
        }

        #endregion

        #region Private Methods

        private string BuildLinkWithFilters(WPEnums.ContentTypes contentType, Dictionary<WPEnums.Filters, string> filterParams, string endPoint)
        {
            //how specific parameters are constructed can be found in the "Apply" extensions for WPEnums.Filters
            if (filterParams != null && filterParams.Count > 0)
            {
                int i = 0;
                foreach (var param in filterParams)
                {
                    var paramValue = param.Value;

                    //Menus are a completely different API; therefore, the following standard for is set for WP REST API V2 plugin
                    if (contentType != WPEnums.ContentTypes.MENU && contentType != WPEnums.ContentTypes.MENUCOLLECTION)
                    {
                        endPoint = i == 0 ? endPoint + "?" : endPoint + "&";
                    }
                    //If the menu id is not an integer, but rather a slug value
                    else if (contentType == WPEnums.ContentTypes.MENU && param.Key == WPEnums.Filters.slug)
                    {
                        paramValue = param.Value.Replace(WPEnums.Filters.slug.Apply(null), null);
                        if (string.IsNullOrEmpty(paramValue))
                        {
                            var menuId = GetDefaultMenu();
                            paramValue = @"/" + menuId;
                        }
                    }

                    //this is the generic implementation for 
                    endPoint = endPoint + paramValue;
                    i++;

                }
            }

            return endPoint;
        }

        private string BuildCacheKey(WPEnums.ContentTypes contentType, string endPoint)
        {
            //this area can be used for anything needed to modify the cache key
            string cacheKey = endPoint;

            return cacheKey;
        }

        private object CacheAndMapWpContent(WPEnums.ContentTypes contentType, Dictionary<WPEnums.Filters, string> filterParams, string endPoint, string cacheKey)
        {
            //object declaration
            object mappedWPContent = null;

            //first, check memory cache for content
            if (!_memoryCache.Contains(cacheKey))
            {
                //call for WP content
                var wpContent = CallForWpContent(endPoint, contentType);

                //due to native links, if content is assumed to be a PAGE and returns nothing, then make the call for as a POST
                if (string.IsNullOrEmpty(wpContent) || wpContent == "[]" && contentType == WPEnums.ContentTypes.PAGE)
                {
                    contentType = WPEnums.ContentTypes.POST;

                    //send the request back through the pipeline as a POST
                    mappedWPContent = GetWPContent(contentType, filterParams);
                }
                else
                {
                    //Map the returned content
                    mappedWPContent = MapWPContent(contentType, filterParams, wpContent);
                }

                //grab the cache expiration for the specified content type
                var expiration = SetCacheExpirationValue(contentType);

                if (!wpContent.Contains("System.Net.WebException") && mappedWPContent != null)
                {
                    //cache the mapped content model object
                    _memoryCache.Add(cacheKey, mappedWPContent, expiration);
                }
            }

            return _memoryCache.Get(cacheKey, null);
        }

        private string CallForWpContent(string endPoint, WPEnums.ContentTypes contentType)
        {
            var post = new JSONRequest();
            string response = null;

            try
            {
                if (MockRequest == "true")
                {
                    response = post.MockWebCall(endPoint, contentType);
                }
                else
                {
                    response = post.WebCall(endPoint);
                }
            }
            catch (Exception ex)
            {
                //whatever you want to do here
            }

            return response;
        }

        private object MapWPContent(WPEnums.ContentTypes contentType, Dictionary<WPEnums.Filters, string> filterParams, string wpContent)
        {
            object model = null;
            if (wpContent != null)
            {
                try
                {
                    //Structured Class Model Mapper
                    model = WPModelMapper.MapWordPressProperties(wpContent, contentType, filterParams);
                }
                catch (Exception ex)
                {
                    //whatever you want to put here
                }
            }

            return model;
        }

        private string GetEndPointPath(WPEnums.ContentTypes contentType)
        {
            var rootSitePath = WPEndPoint;
            string apiPath = null;
            switch (contentType)
            {
                //WP API V2 Plugin Required
                case WPEnums.ContentTypes.PAGE:
                    apiPath = @"/wp-json/wp/v2/pages";
                    break;
                case WPEnums.ContentTypes.POST:
                    apiPath = @"/wp-json/wp/v2/posts";
                    break;
                case WPEnums.ContentTypes.POSTCATEGORY:
                    apiPath = @"/wp-json/wp/v2/posts";
                    break;
                case WPEnums.ContentTypes.CATEGORY:
                    apiPath = @"/wp-json/wp/v2/categories";
                    break;
                case WPEnums.ContentTypes.TAG:
                    apiPath = @"/wp-json/wp/v2/tags";
                    break;
                case WPEnums.ContentTypes.AUTHOR:
                    apiPath = @"/wp-json/wp/v2/author";
                    break;
                //WP API V2 MENU Plugin Required
                case WPEnums.ContentTypes.MENU:
                    apiPath = @"/wp-json/wp-api-menus/v2/menus";
                    break;
                case WPEnums.ContentTypes.MENUCOLLECTION:
                    apiPath = @"/wp-json/wp-api-menus/v2/menus";
                    break;
                case WPEnums.ContentTypes.MENULOCATION:
                    apiPath = @"/wp-json/wp-api-menus/v2/menu-locations";
                    break;
                default:
                    return null;
            }

            return rootSitePath + apiPath;
        }

        /// <summary>
        /// Sets the cache expiration value based on the given content type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private DateTimeOffset SetCacheExpirationValue(WPEnums.ContentTypes contentType)
        {
            int cacheExpirationValue = 30;

            try
            {
                //Set the cache duration for whichever duration you want for any given content type
                cacheExpirationValue = Convert.ToInt32(ConfigurationManager.AppSettings["CacheExpireMins-" + contentType.ToString()]);
                if (cacheExpirationValue <= 0)
                    cacheExpirationValue = 30;
            }
            catch (Exception ex)
            {
                //whatever you want to put for your error handling
                cacheExpirationValue = 30;
            }

            return DateTimeOffset.UtcNow.AddMinutes(cacheExpirationValue);
        }

        private string GetDefaultMenu()
        {
            var menuCollection = (List<WPMenuModel>)GetWPContent(WPEnums.ContentTypes.MENUCOLLECTION, new Dictionary<WPEnums.Filters, string>());
            int menuId = 0;

            if (menuCollection.Any())
            {
                try
                {
                    menuId = menuCollection.Where(a => a.slug.Contains("main")).Select(a => a.ID).First();
                }
                catch (Exception ex)
                {
                    menuId = menuCollection.First().ID;
                }
            }

            return menuId.ToString();
        }
    }
    #endregion
}
