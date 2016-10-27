using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WordPress.Content.Models;

namespace WordPress.Content.Mappers
{
    public class WPMenuBuilder
    {

        #region Public Methods

        public static void AddMenuStyle(WPMenuModel wpMenuItemCollection)
        {
            var menuBuilderSettings = ConfigurationManager.AppSettings["UseMenuBuilder"];
            var useMenuBuilder = !string.IsNullOrEmpty(menuBuilderSettings) ? Convert.ToBoolean(menuBuilderSettings) : false;

            if (useMenuBuilder)
            {
                var MVCMenuDecoration = ConfigurationManager.AppSettings["UseMVCMenuDecoration"];
                var useMVCDecoration = !string.IsNullOrEmpty(MVCMenuDecoration)
                    ? Convert.ToBoolean(MVCMenuDecoration)
                    : false;

                switch (useMVCDecoration)
                {
                    case true:
                        AddMVCMenuStyle(wpMenuItemCollection);
                        break;
                    default:
                        AddWPMenuStyle(wpMenuItemCollection);
                        break;

                }
            }
        }

        #endregion

        #region Private Methods

        #region WordPress Menu Styles

        /// <summary>
        /// Build the UI decorations for the WordPress Menu
        /// </summary>
        /// <param name="wpMenuItemCollection"></param>
        private static void AddWPMenuStyle(WPMenuModel wpMenuItemCollection)
        {
            //menu decoration for the overall menu level
            if (wpMenuItemCollection != null && wpMenuItemCollection.items != null && wpMenuItemCollection.items.Any())
            {
                wpMenuItemCollection.styles = new Dictionary<string, string>();
                wpMenuItemCollection.styles.Add("divTopMenuClass", "class=main-menu");
                wpMenuItemCollection.styles.Add("ulTopMenuId", "id =menu-dropdown-menu-1-0");
                wpMenuItemCollection.styles.Add("ulTopMenuClass", "class=menu");
                foreach (var wpContentItem in wpMenuItemCollection.items)
                {
                    //menu decoration for the top level menu items
                    AddWPMenuStyle(wpContentItem);
                }
            }
        }


        /// <summary>
        /// This method is separated so when it is reused that it does not decorate with menu level styles
        /// </summary>
        /// <param name="wpMenuItemCollection"></param>
        private static void AddWPMenuStyle(WPMenuModel.Item[] wpMenuItemCollection)
        {
            if (wpMenuItemCollection != null && wpMenuItemCollection.Any())
            {
                foreach (var wpContentItem in wpMenuItemCollection)
                {
                    //menu decoration for the top level menu items
                    AddWPMenuStyle(wpContentItem);
                }
            }
        }

        private static void AddWPMenuStyle(WPMenuModel.Item wpMenuItem)
        {
            //variable declartions
            var wpItem = new Dictionary<string, string>();
            string wpId = wpMenuItem.id.ToString();
            string wpType = wpMenuItem.type;
            string wpObject = wpMenuItem.type_label.ToLower();
            var parentID = wpMenuItem.parent;
            string liBaseMenuClassChild = null;
            string liBaseMenuClassSubMenu = null;

            if (wpMenuItem.children != null)
            {
                //menu decoration for the sub level menu items
                wpItem.Add("ulSubMenuClass" + "-" + wpId, "class=sub-menu");
                liBaseMenuClassChild = "menu-item-has-children";
                AddWPMenuStyle(wpMenuItem.children);
            }

            liBaseMenuClassSubMenu = parentID == 0 ? "dropdown-menu" : "dropdown-submenu";

            wpItem.Add("liBaseMenuId" + "-" + wpId, string.Format("id = menu-item-{0}", wpId));
            wpItem.Add("liBaseMenuClass" + "-" + wpId, string.Format("class=\"menu-item menu-item-type-{0} menu-item-object-{1} {2} menu-item-{3} {4}\"", wpType, wpObject, liBaseMenuClassChild, wpId, liBaseMenuClassSubMenu));

            wpMenuItem.styles = wpItem;

        }

        #endregion

        #region MVC Menu Styles 

        /// <summary>
        /// Build the UI decorations for the WordPress Menu
        /// </summary>
        /// <param name="wpMenuItemCollection"></param>
        private static void AddMVCMenuStyle(WPMenuModel wpMenuItemCollection)
        {
            //menu decoration for the overall menu level
            if (wpMenuItemCollection != null && wpMenuItemCollection.items != null && wpMenuItemCollection.items.Any())
            {
                wpMenuItemCollection.styles = new Dictionary<string, string>();
                wpMenuItemCollection.styles.Add("divTopMenuClass", "class='navbar-collapse collapse'");
                wpMenuItemCollection.styles.Add("ulTopMenuId", null);
                wpMenuItemCollection.styles.Add("ulTopMenuClass", "class='nav navbar-nav'");
                foreach (var wpContentItem in wpMenuItemCollection.items)
                {
                    //menu decoration for the top level menu items
                    AddMVCMenuStyle(wpContentItem);
                }
            }
        }

        /// <summary>
        /// This method is separated so when it is reused that it does not decorate with menu level styles
        /// </summary>
        /// <param name="wpMenuItemCollection"></param>
        private static void AddMVCMenuStyle(WPMenuModel.Item[] wpMenuItemCollection)
        {
            if (wpMenuItemCollection != null && wpMenuItemCollection.Any())
            {
                foreach (var wpContentItem in wpMenuItemCollection)
                {
                    //menu decoration for the top level menu items
                    AddMVCMenuStyle(wpContentItem);
                }
            }
        }

        private static void AddMVCMenuStyle(WPMenuModel.Item wpMenuItem)
        {
            //variable declartions
            var wpItem = new Dictionary<string, string>();
            string wpId = wpMenuItem.id.ToString();
            string wpType = wpMenuItem.type;
            string wpObject = wpMenuItem.type_label.ToLower();
            var parentID = wpMenuItem.parent;
            string liBaseMenuClassChild = null;
            string liBaseMenuClassSubMenu = null;

            if (wpMenuItem.children != null)
            {
                //menu decoration for the sub level menu items
                wpItem.Add("ulSubMenuClass" + "-" + wpId, "class=dropdown-menu");
                liBaseMenuClassChild = "dropdown";
                AddMVCMenuStyle(wpMenuItem.children);
            }

            liBaseMenuClassSubMenu = parentID == 0 ? "" : "dropdown";

            wpItem.Add("liBaseMenuId" + "-" + wpId, null);
            string liBaseMenuClass = null;

            if (!string.IsNullOrEmpty(liBaseMenuClassChild))
            {
                liBaseMenuClass = string.Format("class=\"{0}\"", liBaseMenuClassChild);
            }

            wpItem.Add("liBaseMenuClass" + "-" + wpId, liBaseMenuClass);

            wpMenuItem.styles = wpItem;

        }

        #endregion

        #endregion

    }
}
