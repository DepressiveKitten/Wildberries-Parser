using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wildberries.Parsing.Interfaces;
using Wildberries.Parsing.SearchPage.Interfaces;

namespace Wildberries.Parsing.SearchPage
{
    /// <inheritdoc/>
    public class SearchPageReader : IItemsReader
    {
        private static readonly string UrlAccessError = "Unable to get response from {0}";
        private static readonly string CardCommonName = "product-card";
        private static readonly string CardBannedName = "advert-card-item";
        private static readonly string ClassName = "class";
        private static readonly string HrefNode = "a";
        private static readonly string CardNode = "div";
        private static readonly string SiteAddressKeyName = "site-address";

        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPageReader"/> class.
        /// </summary>
        /// <param name="parser">parser to get <see cref="SiteItem"/> from page.</param>
        /// <param name="appSettings">project configuration.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="validator">Validate site items.</param>
        public SearchPageReader(IHtmlParser parser, ILogger logger, IConfiguration appSettings, ISiteItemValidator validator)
        {
            this.Logger = logger;
            this.AppSettings = appSettings;
            this.Parser = parser;
            this.client = new HttpClient();
        }

        private ILogger Logger { get; }

        private IConfiguration AppSettings { get; }

        private IHtmlParser Parser { get; }

        private ISiteItemValidator Validator { get; }

        /// <inheritdoc/>
        public IAsyncEnumerable<SiteItem> Read(string searchKey)
        {
            if (searchKey is null)
            {
                throw new ArgumentNullException(nameof(searchKey));
            }

            string url = this.AppSettings[SiteAddressKeyName] + searchKey;

            return this.ReadCore(url);
        }

        private async IAsyncEnumerable<SiteItem> ReadCore(string url)
        {
            string htmlContent = await this.GetHtmlContent(url);
            if (htmlContent is null)
            {
                yield break;
            }

            System.Console.WriteLine(htmlContent);

            foreach (string itemUrl in this.GetAllItemsURL(htmlContent))
            {
                htmlContent = await this.GetHtmlContent(itemUrl);

                if (htmlContent != null)
                {
                    yield return this.Parser.Parse(htmlContent);
                }
            }
        }

        private async Task<string> GetHtmlContent(string url)
        {
            HttpResponseMessage response = await this.client.GetAsync(url);
            if (response.IsSuccessStatusCode is false)
            {
                this.Logger.LogError(string.Format(UrlAccessError, url));
                return null;
            }

            this.Logger.LogTrace(string.Format("Get response from {0}", url));
            return await response.Content.ReadAsStringAsync();
        }

        // TODO: load text with data and parse items ids
        private IEnumerable<string> GetAllItemsURL(string htmlBody)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlBody);

            return from div in htmlDocument.DocumentNode.Descendants(CardNode)
                   where div.GetAttributeValue(ClassName, string.Empty).Contains(CardCommonName) && !div.GetAttributeValue(ClassName, string.Empty).Contains(CardBannedName)
                   where div.SelectSingleNode(HrefNode).GetAttributeValue(ClassName, string.Empty) != string.Empty
                   select div.SelectSingleNode(HrefNode).GetAttributeValue(ClassName, string.Empty);
        }
    }
}
