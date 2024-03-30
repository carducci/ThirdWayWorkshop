using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using HtmlAgilityPack;

namespace ThirdWay.Feed
{
    internal class ItemParser
    {
        private readonly CodeHollow.FeedReader.Feed _feed;

        public ItemParser(CodeHollow.FeedReader.Feed feed)
        {
            _feed = feed;
        }

        public string GetHeroImage(FeedItem item)
        {
            var body = ConvertRelativeToAbsolute(_feed.Link, item.Content);
            return FindHeroImage(body);
        }

        private string FindHeroImage(string htmlFragment)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlFragment);

            // Select the first <img> element
            var imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img");

            return imgNode.GetAttributeValue("src", string.Empty);
        }

        public string ParseBody(FeedItem item)
        {
            var body = ConvertRelativeToAbsolute(_feed.Link, item.Content);
            body = ConvertAbsoluteToInternal(_feed.Link, body);
            body = RemoveHeroImage(body);
            return body;
        }

        private string ConvertRelativeToAbsolute(string baseUrl, string htmlFragment)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlFragment);
            var baseUri = new Uri(baseUrl);

            // Select all <a> and <img> elements with href or src attributes
            var linkNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href] | //img[@src]");
            if (linkNodes == null) return htmlDocument.DocumentNode.OuterHtml;
            foreach (var node in linkNodes)
            {
                // For each node, check whether it's an <a> tag with an href attribute or an <img> tag with a src attribute
                var attribute = node.Name.Equals("a", StringComparison.OrdinalIgnoreCase) ? "href" : "src";

                // Get the current attribute value
                var value = node.GetAttributeValue(attribute, string.Empty);

                // Create a Uri instance for the attribute value
                if (!Uri.TryCreate(baseUri, value, out var absoluteUri)) continue;
                node.SetAttributeValue(attribute,
                    node.Name.Equals("a", StringComparison.OrdinalIgnoreCase)
                        ? $"/post/hash/{Utilities.GetHashFromString(absoluteUri.ToString().ToLower())}"
                        : absoluteUri.ToString());
            }

            // Return the modified HTML as a string
            return htmlDocument.DocumentNode.OuterHtml;
        }

        private string ConvertAbsoluteToInternal(string baseUrl, string htmlFragment)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlFragment);
            var baseUri = new Uri(baseUrl);

            var linkNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes != null)
            {
                foreach (var node in linkNodes)
                {

                    var href = node.GetAttributeValue("href", string.Empty);
                    if (Uri.TryCreate(href, UriKind.Absolute, out var absoluteUri))
                    {
                        // Check if the absolute URL belongs to the base URL domain
                        if (baseUri.Host.Equals(absoluteUri.Host, StringComparison.OrdinalIgnoreCase))
                        {
                            // Rewrite the URL using the hash of the absolute URL
                            var newHref = $"/post/hash/{Utilities.GetHashFromString(absoluteUri.ToString().ToLower())}";
                            node.SetAttributeValue("href", newHref);
                        }
                    }
                }
            }

            return htmlDocument.DocumentNode.OuterHtml;
        }

        private string RemoveHeroImage(string htmlFragment)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlFragment);

            // Select the first <img> element
            var imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img");
            if (imgNode != null)
            {
                imgNode.Remove();
            }

            return htmlDocument.DocumentNode.OuterHtml;
        }
    }
}
