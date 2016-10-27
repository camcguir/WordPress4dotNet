# WordPress4dotNet

This project is dedicated to bridging the gap between WordPress and .NET combined sites, through the use of WP REST API V2.  The project also makes use of WP API Menus.  

<h2>Purpose:</h2>
To construct a .NET Provider for content extracted from any WordPress Content Management System using WP REST API V2, WP API Menus, and compatible plugins.  

<h2>Design Theory:</h2>
<ul>
	<li>Design one process and leverage as many reusable methods as possible and handle exceptions on an individual basis, while limiting outbound calls.</li>
	<li>Use auto deserialization by Newtonsoft into structured classes for easy mapping.</li>
	<li>Build custom extensions to allow for safe null checking and safe content extraction from buried model structures.</li>
	<li>Menus are the only programmatically decorated HTML elements (optional), and are based on WP menu standards and theme add-ons.</li>
	<li>Manual page scraping will need to occur to build the templates for specific WP themes/content types for CSS Styles, Javascript, and Design Styles.</li>

	Though this component is included in an MVC project, it's merely used as an example implementation.  It can also be used in classic web forms ASP.NET.  The WordPress.Content project is really the meat and potatoes of what is happening.  This project is really just the initial groundwork intended to be flexible enough to allow for several design aspects.  
	<ul>
		<li>Match template names from WordPress to View Names in MVC and have views programtically assigned through the use of WordPress</li>
		<li>Make an AJAX call to the provider to grab content from the client-side and display in an fashion desire.  Of course, you'll need to build an action in your controller for that, but again.. the flexibility is there.</li>
		<li>Use other data attributes from WordPress to drive data and UI elements in your MVC application</li>
		<li>Add dependency injection, your own caching, and/or your own error/exception handlers</li>
	</ul>
</ul>
 
 <h2>Application Dependences:</h2>
 <h3>.NET Dependencies</h3>
 <ul>
	<li>.NET Framework 4.5 and newer</li>
	<li>NewtonSoft 9.0 or newer</li>
 </ul>
 <h3>WordPress Instance Dependencies</h3>
 The following plugins must be "Installed" and "Activated" on your WordPress instance
 <ul>
	<li><a href="https://wordpress.org/plugins/rest-api/" target="_blank">WP REST API (Version 2)</a></li>
	<li><a href="https://wordpress.org/plugins/wp-api-menus/" target="_blank">WP API Menus</a></li>
 </ul>
 WP REST API Documentation can be found at: http://v2.wp-api.org/
 OR
 **If WP REST API V2 is installed on your instance, reference information can be found at the WP-JSON root: http://{yourSite}/wp-json

 <h2>Other Notes:</h2>

 <h3>Current Gaps:</h3>

 <ul>
 <li>Terms: not retrieving terms; requirement will be based on theme and theme usage</li>
 <li>Comments: not retrieving/mapping/displaying comments</li>
 <li>Extended plugins not included: sliders, widgets, sidebars, etc.</li>
 <li>Only READ operations are configured; all other CRUD operations have not been included in this project</li>
 </ul>

 <h3>Caching:</h3>

 <ul>
 <li>.NET memory cache is currently being used, though other caching solutions can be used instead.</li>
 <li>An alternative to populating the memory cache with the entire JSON response, is to physically cache the JSON response as a flat file, then consume the flat file response, while only caching the JSON flat file path and still being able to use expiration mins
 </ul>

 <h3>3rd Party Libraries:</h3>

 <ul>
 <li>Dependency injection was not used to avoid conflicting frameworks</li>
 <li>Newtonsoft is the only 3rd party library used</li>
 </ul>

 <h2>Diagrams</h2>
 For diagrams, please select the files that are located in the root of the project.  These drawings were created at the time of the initial commit.

 <h2>App Settings Dictionary</h2>
 "TopNavMenuID" = The Menu Id of the top navigation menu.  Once WP API MENUS is installed, you can find the appropriate menu id using the standard json request.
 "FootNavMenuID" = The Menu Id of the footer menu.  Once WP API MENUS is installed, you can find the appropriate menu id using the standard json request.
 "WPEndPoint" = The URL end point of your WordPress instance (ex. http://wordpress.org)
 "UseMenuBuilder" = An true/false setting granting option to use the MenuBuilder to decorate your menu object with HTML Attributes before being returned to the view.  You can choose to use the MenuBuilder class or doctor your menus manually in the view.
 "UseMVCMenuDecoration" = A true/false setting granting the option to use decoration styles for the OTB MVC Menu or the preconfigured WP decorations
 "CacheExpireMins-MENU" = Number of mins to set the cache expiration for MENU content
 "CacheExpireMins-POST" = Number of mins to set the cache expiration for POST content
 "CacheExpireMins-PAGE" = Number of mins to set the cache expiration for PAGE content
 "CacheExpireMins-POSTCATEGORY" = Number of mins to set the cache expiration for a collection of POST content
 "CacheExpireMins-MENUCOLLECTION" = Number of mins to set the cache expiration for a collection of MENU content
 "MockRequest" = While testing, you can locally save WP REST API repsonses.  This is a true/false setting that allows you to turn off/on the mock request.
 "MockPostLocation" = File name and location for POST content when using the MockRequest
 "MockPageLocation" = File name and location for PAGE content when using the MockRequest
 "MockCategoryLocation" = File name and location for CATEGORY content when using the MockRequest
 "MockPostCategoryLocation" = File name and location for a collection of POST content when using the MockRequest
 "MockTopMenuLocation" = File name and location for top nav MENU content when using the MockRequest
 "MockFootMenuLocation" = File name and location for footer MENU content when using the MockRequest
 "MockMenuCollection" = File name and location for a collection of MENU content when using the MockRequest
 "proxyAddress" = if your outbound requests go through a gateway or proxy, this is the proxy server address
 "proxyDomain" = if your outbound requests go through a gateway or proxy, this is the proxy server credentials domain
 "proxyUsername" = if your outbound requests go through a gateway or proxy, this is the proxy server credentials username
 "proxyPassword" = if your outbound requests go through a gateway or proxy, this is the proxy server credentials password
 "proxyTimeout" = if your outbound requests go through a gateway or proxy, this is the proxy server timeout settings (milliseconds)

