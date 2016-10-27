using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WordPress.Content.Models;

namespace WordPress.Content.Helpers
{
    public class JSONRequest
    {
        private static int _mTimeout()
        {
            int defaultValue = 20000;
            int.TryParse(ConfigurationManager.AppSettings["proxyTimeout"], out defaultValue);
            return defaultValue;
        }

        private static string _mProxy = ConfigurationManager.AppSettings["proxyAddress"];
        private static string _mProxyDomain = ConfigurationManager.AppSettings["proxyDomain"];
        private static string _mProxyUser = ConfigurationManager.AppSettings["proxyUsername"];
        private static string _mProxyPass = ConfigurationManager.AppSettings["proxyPassword"];

        /// <summary>
        /// Mock service method
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public string MockWebCall(string path, WPEnums.ContentTypes contentType)
        {
            string response = null;
            switch (contentType)
            {
                case WPEnums.ContentTypes.CATEGORY:
                    response = MockCategoryResponse();
                    break;
                case WPEnums.ContentTypes.PAGE:
                    response = MockPageResponse();
                    break;
                case WPEnums.ContentTypes.POST:
                    response = MockPostResponse();
                    break;
                case WPEnums.ContentTypes.POSTCATEGORY:
                    response = MockPostCategoryResponse();
                    break;
                //Requires WP REST API V2 MENUS
                case WPEnums.ContentTypes.MENU:
                    response = MockMenuResponse(path);
                    break;
                case WPEnums.ContentTypes.MENUCOLLECTION:
                    response = MockMenuCollectionResponse(path);
                    break;
            }

            return response;

        }

        /// <summary>
        /// Standard Json RESTFUL service.  Using request methods: "GET" & "POST"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public string WebCall(string path, string json = null)
        {
            string result = null;
            var request = (HttpWebRequest)WebRequest.Create(path);
            if (!String.IsNullOrEmpty(_mProxy))
            {
                var webProxy = new WebProxy(_mProxy, true);
                webProxy.Credentials = new System.Net.NetworkCredential(_mProxyUser, _mProxyPass, _mProxyDomain);
                request.Proxy = webProxy;
            }
            request.Timeout = _mTimeout();

            request.ContentType = "application/json";
            
            try
            {
                if (json != null)
                {
                    request.Method = "POST";
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }
                }
                else
                {
                    request.Method = "GET";
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                result = path + " : " + ex;
            }

            return result;

        }
        private string MockPostResponse()
        {
            var post = ConfigurationManager.AppSettings["MockPostLocation"];
            return File.ReadAllText(post).ToString();
        }
        private string MockPostCategoryResponse()
        {
            var postCategory = ConfigurationManager.AppSettings["MockPostCategoryLocation"];
            return File.ReadAllText(postCategory).ToString();
        }

        private string MockMenuResponse(string path = null)
        {
            var topMenuValue = ConfigurationManager.AppSettings["TopNavMenuID"];
            if (path.Contains(topMenuValue))
            {
                var topMenu = ConfigurationManager.AppSettings["MockTopMenuLocation"];
                return File.ReadAllText(topMenu).ToString();
            }
            else
            {
                var footMenu = ConfigurationManager.AppSettings["MockFootMenuLocation"];
                return File.ReadAllText(footMenu).ToString();
            }
        }

        private string MockMenuCollectionResponse(string path = null)
        {
            var menuCollection = ConfigurationManager.AppSettings["MockMenuCollection"];
            return File.ReadAllText(menuCollection).ToString();
        }

        private string MockCategoryResponse()
        {
            var category = ConfigurationManager.AppSettings["MockCategoryLocation"];
            return File.ReadAllText(category).ToString();
        }

        private string MockPageResponse()
        {
            var page = ConfigurationManager.AppSettings["MockPageLocation"];
            return File.ReadAllText(page).ToString();
        }
    }
}