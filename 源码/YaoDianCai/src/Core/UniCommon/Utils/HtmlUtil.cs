using System;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Security.Application;


namespace Uni.Core.Common.Utils
{
    public class HtmlUtil
    {
        private static readonly Regex htmlTags = new Regex(@"</?(H|h)(T|t)(M|m)(L|l)(.|\n)*?>");
        private static readonly Regex HtmlTagReplacer = new Regex(@"</?(.|\n)*?>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex HtmlCommentsReplacer = new Regex("<!--(?s).*?-->", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex XssTagReplacer = new Regex(@"<\s*(style|script)[^>]*>(.*?)<\s*/\s*(style|script)>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Singleline);


        public static string SanitizeFragment(string htmlText)
        {
            return Sanitizer.GetSafeHtmlFragment(htmlText);
        }

        public static string SanitizeHtml(string htmlText)
        {
            return Sanitizer.GetSafeHtml(htmlText);
        }

        public static string ToPlainText(string html)
        {
            return GetText(html);
        }

        public static string GetText(string html)
        {
            return GetText(html, 0);
        }

        public static string GetText(string html, bool preserveSpaces)
        {
            return GetText(html, 0, "...", true, preserveSpaces);
        }

        public static string GetText(string html, int maxLength)
        {
            return GetText(html, maxLength, "...");
        }

        public static string GetText(string html, int maxLength, bool preserveSpaces)
        {
            return GetText(html, maxLength, "...", true, preserveSpaces);
        }

        public static string GetText(string html, int maxLength, string endBlockTemplate)
        {
            return GetText(html, maxLength, endBlockTemplate, true);
        }

        public static string GetText(string html, int maxLength, string endBlockTemplate, bool calcEndBlockTemplate)
        {
            return GetText(html, maxLength, endBlockTemplate, calcEndBlockTemplate, false);
        }

        public static string GetText(string html, int maxLength, string endBlockTemplate, bool calcEndBlockTemplate, bool preserveSpaces)
        {
            string unformatedText = string.Empty;
            if (!string.IsNullOrEmpty(html))
            {
                //Clean malicious tags. <script> <style> etc
                html = XssTagReplacer.Replace(html, string.Empty);

                if (string.IsNullOrEmpty(html)) //return this if it's empty
                {
                    return html;
                }

                try
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    unformatedText = doc.DocumentNode.InnerText;
                }
                catch (Exception)
                {
                    unformatedText = HtmlTagReplacer.Replace(XssTagReplacer.Replace(html, ""), "");
                }

                if (!string.IsNullOrEmpty(unformatedText))
                {
                    //Kill comments
                    unformatedText = HtmlCommentsReplacer.Replace(unformatedText, string.Empty);
                    unformatedText = unformatedText.Trim('\r', '\n', ' ');//Trim spaces and line breaks

                    if (!string.IsNullOrEmpty(unformatedText))
                    {
                        if (maxLength == 0 || unformatedText.Length < maxLength)
                        {
                            return HttpUtility.HtmlDecode(unformatedText);
                        }
                        //Set maximum length with end block
                        maxLength = Math.Max(0, calcEndBlockTemplate ? maxLength - endBlockTemplate.Length : maxLength);
                        int startIndex = Math.Max(0, Math.Min(unformatedText.Length - 1, maxLength));
                        int countToScan = Math.Max(0, startIndex - 1);

                        int lastSpaceIndex = unformatedText.LastIndexOf(' ', startIndex, countToScan);

                        unformatedText = lastSpaceIndex > 0 && lastSpaceIndex < unformatedText.Length
                                             ? unformatedText.Remove(lastSpaceIndex)
                                             : unformatedText.Substring(0, maxLength);

                        if (!string.IsNullOrEmpty(endBlockTemplate))
                        {
                            unformatedText += endBlockTemplate;
                        }
                    }
                }
            }

            return HttpUtility.HtmlDecode(unformatedText);
        }
    }
}
