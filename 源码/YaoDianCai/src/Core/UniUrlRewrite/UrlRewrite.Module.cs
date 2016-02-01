using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using UrlRewrite.Config;
using System.Text.RegularExpressions;
using System.IO;

namespace UrlRewrite.Module
{
    public class RewriterModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            // WARNING!  This does not work with Windows authentication!
            // If you are using Windows authentication, change to app.BeginRequest
            app.AuthorizeRequest += new EventHandler(this.URLRewriter);
        }

        protected void URLRewriter(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string requestedPath = app.Request.Path;

            // get the configuration rules
            UrlsCollection rules = UrlsConfig.GetConfig().Urls;

            for (int i = 0; i < rules.Count; i++)
            {
                // get the pattern to look for, and Resolve the Url (convert ~ into the appropriate directory)
                string lookFor = "^" + RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, rules[i].VirtualUrl) + "$";

                Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);
                if (re.IsMatch(requestedPath))
                {
                    string sendToUrl = RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, re.Replace(requestedPath, rules[i].DestinationUrl));
                    
                    RewriterUtils.RewriteUrl(app.Context, sendToUrl);
                    break;
                }
            }

        }

        public void Dispose() { }
    }



    /// <summary>
    /// Provides utility helper methods for the rewriting HttpModule and HttpHandler.
    /// </summary>
    /// <remarks>This class is marked as internal, meaning only classes in the same assembly will be
    /// able to access its methods.</remarks>
    internal class RewriterUtils
    {
        #region RewriteUrl
        /// <summary>
        /// Rewrite's a URL using <b>HttpContext.RewriteUrl()</b>.
        /// </summary>
        /// <param name="context">The HttpContext object to rewrite the URL to.</param>
        /// <param name="sendToUrl">The URL to rewrite to.</param>
        internal static void RewriteUrl(HttpContext context, string sendToUrl)
        {
            string x, y;
            RewriteUrl(context, sendToUrl, out x, out y);
        }

        /// <summary>
        /// Rewrite's a URL using <b>HttpContext.RewriteUrl()</b>.
        /// </summary>
        /// <param name="context">The HttpContext object to rewrite the URL to.</param>
        /// <param name="sendToUrl">The URL to rewrite to.</param>
        /// <param name="sendToUrlLessQString">Returns the value of sendToUrl stripped of the querystring.</param>
        /// <param name="filePath">Returns the physical file path to the requested page.</param>
        internal static void RewriteUrl(HttpContext context, string sendToUrl, out string sendToUrlLessQString, out string filePath)
        {
            // see if we need to add any extra querystring information
            if (context.Request.QueryString.Count > 0)
            {
                if (sendToUrl.IndexOf('?') != -1)
                    sendToUrl += "&" + context.Request.QueryString.ToString();
                else
                    sendToUrl += "?" + context.Request.QueryString.ToString();
            }

            // first strip the querystring, if any
            string queryString = String.Empty;
            sendToUrlLessQString = sendToUrl;
            if (sendToUrl.IndexOf('?') > 0)
            {
                sendToUrlLessQString = sendToUrl.Substring(0, sendToUrl.IndexOf('?'));
                queryString = sendToUrl.Substring(sendToUrl.IndexOf('?') + 1);
            }

            // grab the file's physical path
            filePath = string.Empty;
            filePath = context.Server.MapPath(sendToUrlLessQString);

            //iis7 获取乱码问题
            var list = queryString.Split('&');
            var newQueryStr = string.Empty;
            for (int i = 0; i < list.Length; i++)
            {
                var arr = list[i].Split('=');
                if (arr.Length > 1)
                {
                    newQueryStr = newQueryStr + arr[0] + "=" + context.Server.UrlEncode(arr[1]) + "&";

                }
                else
                {
                    newQueryStr = newQueryStr + "&";
                }
            }

            // rewrite the path...
            context.RewritePath(sendToUrlLessQString, String.Empty, newQueryStr.TrimEnd('&'));
            
        }
        #endregion

        /// <summary>
        /// Converts a URL into one that is usable on the requesting client.
        /// </summary>
        /// <remarks>Converts ~ to the requesting application path.  Mimics the behavior of the 
        /// <b>Control.ResolveUrl()</b> method, which is often used by control developers.</remarks>
        /// <param name="appPath">The application path.</param>
        /// <param name="url">The URL, which might contain ~.</param>
        /// <returns>A resolved URL.  If the input parameter <b>url</b> contains ~, it is replaced with the
        /// value of the <b>appPath</b> parameter.</returns>
        internal static string ResolveUrl(string appPath, string url)
        {


            if (url.Length == 0 || url[0] != '~')
                return url;		// there is no ~ in the first character position, just return the url
            else
            {
                if (url.Length == 1)
                    return appPath;  // there is just the ~ in the URL, return the appPath
                if (url[1] == '/' || url[1] == '\\')
                {
                    // url looks like ~/ or ~\
                    if (appPath.Length > 1)
                        return appPath + "/" + url.Substring(2);
                    else
                        return "/" + url.Substring(2);
                }
                else
                {
                    // url looks like ~something
                    if (appPath.Length > 1)
                        return appPath + "/" + url.Substring(1);
                    else
                        return appPath + url.Substring(1);
                }
            }
        }
    }
}
