using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPress.Content.Models
{
    public static class WPEnums
    {
        #region Public Enums

        /// <summary>
        /// Content Types
        /// </summary>
        public enum ContentTypes
        {
            POST,
            PAGE,
            MENU,
            MENULOCATION,
            MENUCOLLECTION,
            CATEGORY, 
            POSTCATEGORY,
            POSTMEDIA,
            TAG,
            TAGCOLLECTION,
            AUTHOR
        }

        /// <summary>
        /// For a full list of available filter arguments, access the WP REST API @ http://{domain}/wp-json
        /// </summary>
        public enum Filters
        {
            /// <summary>
            /// Scope under which the request is made; determines fields present in response.
            /// | VALUES: One of: view (default), embed, edit
            /// | For Content Types: Pages, Posts
            /// </summary>
            context,
            /// <summary>
            /// Current page of the collection.
            /// | VALUES: Numeric (default: 1)
            /// | For Content Types: Pages, Posts
            /// </summary>
            page,
            /// <summary>
            /// Maximum number of items to be returned in result set.
            /// | VALUES: Numeric (default: 10)
            /// | For Content Types: Pages, Posts
            /// </summary>
            per_page,
            /// <summary>
            /// Limit results to those matching a string.
            /// | VALUES: String (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            search,
            /// <summary>
            /// Limit response to resources published after a given ISO8601 compliant date.
            /// | For Content Types: Pages, Posts
            /// </summary>
            after,
            /// <summary>
            /// Limit result set to posts assigned to specific authors.
            /// | VALUES: String (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            author,
            /// <summary>
            /// Ensure result set excludes posts assigned to specific authors.
            /// | VALUES: String (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            author_exclude,
            /// <summary>
            /// Limit response to resources published before a given ISO8601 compliant date.
            /// | For Content Types: Pages, Posts
            /// </summary>
            before,
            /// <summary>
            /// Ensure result set excludes specific ids.
            /// | VALUES: Numeric (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            exclude,
            /// <summary>
            /// Ensure result set includes specific ids.
            /// | VALUES: Numeric (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            include,
            /// <summary>
            /// Offset the result set by a specific number of items.
            /// | VALUES: Numeric (default: "")
            /// | For Content Types: Pages, Posts
            /// </summary>
            offest,
            /// <summary>
            /// Order sort attribute ascending or descending.
            /// | VALUES: One of: desc (default), asc
            /// | For Content Types: Pages, Posts
            /// </summary>
            order,
            /// <summary>
            /// Sort collection by object attribute.
            /// | VALUES: One of: date (default), id, include, title, slug
            /// | For Content Types: Pages, Posts
            /// </summary>
            orderby,
            /// <summary>
            /// Limit result set to posts with a specific slug.
            /// | VALUES: String
            /// | For Content Types: Pages, Posts
            /// </summary>
            slug,
            /// <summary>
            /// Limit result set to posts assigned a specific status.
            /// | VALUES: String (default: "publish")
            /// | For Content Types: Pages, Posts
            /// </summary>
            status,
            /// <summary>
            /// Use WP Query arguments to modify the response; private query vars require appropriate authorization.
            /// | VALUES: String 
            /// | For Content Types: Pages, Posts
            /// </summary>
            filter,
            /// <summary>
            /// Limit result set to resources with a specific menu_order value.
            /// | VALUES: Numeric 
            /// | For Content Types: Pages
            /// </summary>
            menu_order,
            /// <summary>
            /// Limit result set to those of particular parent ids.
            /// | VALUES: Numeric 
            /// | For Content Types: Pages
            /// </summary>
            parent,
            /// <summary>
            /// Limit result set to all items except those of a particular parent id.
            /// | VALUES: Numeric 
            /// | For Content Types: Pages
            /// </summary>
            parent_exclude,
            /// <summary>
            /// Limit result set to all items that have the specified term assigned in the categories taxonomy.
            /// | VALUES: Numeric 
            /// | For Content Types: Posts
            /// </summary>
            categories,
            /// <summary>
            /// Limit result set to all items that have the specified term assigned in the tags taxonomy.
            /// | VALUES: String 
            /// | For Content Types: Posts
            /// </summary>
            tags,
            /// <summary>
            /// Limit result set to all menu items that have the specified id assigned.
            /// | VALUES: Numeric 
            /// | For Content Types: Menus
            /// </summary>
            menuID,
            /// <summary>
            /// Global parameter for providing additional data parameters to the response.
            /// | VALUES: N/A 
            /// | For Content Types: ALL
            /// </summary>
            _embed
        }

        #endregion

        /// <summary>
        /// Used to apply filters in Filter Dictionary
        /// Example:
        /// var filterParams = new Dictionary (WPEnums.Filters, string)... 
        /// filterParams.Add(WPEnums.Filters.include, WPEnums.Filters.include.Apply(string));
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string Apply(this WPEnums.Filters filter, string parameter, string customProperty = null)
        {
            #region Syntax Rules for Conditional Params

            if (!string.IsNullOrEmpty(parameter))
            {
                parameter = parameter.Replace(WPEnums.Filters.slug.ToString() + "=", "");
            }

            switch (filter)
            {
                //syntax rules for menu
                case WPEnums.Filters.menuID:
                    return "/" + parameter;
                //syntax rules for _embed
                case WPEnums.Filters._embed:
                    return WPEnums.Filters._embed.ToString();
                //syntax rules for filters
                case WPEnums.Filters.filter:
                    var slugArray = parameter.Split('/');
                    if (slugArray.Count() > 0)
                    {
                        var slugList = slugArray.Where(a => !string.IsNullOrEmpty(a)).ToList();
                        parameter = slugList.LastOrDefault();
                    }
                    return string.Format("{0}[{1}]={2}", WPEnums.Filters.filter.ToString(), customProperty, parameter);
                default:
                    return filter.ToString() + "=" + parameter;
            }

            #endregion
        }
    }
}
