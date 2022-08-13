using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wildberries.Parsing.Interfaces;
using Wildberries.Parsing.JsonSearchPage.Interfaces;

namespace Wildberries.Parsing.JsonSearchPage
{
    /// <inheritdoc/>
    public class SearchPageJsonReader : IItemsReader
    {
        private static readonly string UrlAccessError = "Unable to get response from {0}";
        private static readonly string SuccesInfoString = "From key: {0}, Recieved: {1} valid items";
        private static readonly string AddressSegment = "search-page-json";
        private static readonly string AddressKey = "page-address";
        private static readonly string AddressParametersKey = "parameters";
        private static readonly string ParametersSeparator = "&";
        private static readonly string ParametersValueSign = "=";

        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPageJsonReader"/> class.
        /// </summary>
        /// <param name="appSettings">project configuration.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="validator">Validate site items.</param>
        public SearchPageJsonReader(ILogger logger, IConfiguration appSettings, ISiteItemValidator validator, IJsonParser parser)
        {
            this.Logger = logger;
            this.AppSettings = appSettings;
            this.Parser = parser;
            this.Validator = validator;
            this.client = new HttpClient();
        }

        private ILogger Logger { get; }

        private IConfiguration AppSettings { get; }

        private ISiteItemValidator Validator { get; }

        private IJsonParser Parser { get; }

        /// <inheritdoc/>
        public IAsyncEnumerable<SiteItem> Read(string searchKey)
        {
            if (searchKey is null)
            {
                throw new ArgumentNullException(nameof(searchKey));
            }

            return this.ReadCore(searchKey, this.CreateUrl(searchKey));
        }

        private async IAsyncEnumerable<SiteItem> ReadCore(string key ,string url)
        {
            var jsonResponse = await GetContent(url);

            int successfulItemsAmount = 0;

            foreach (var item in Parser.Parse(jsonResponse))
            {
                var validationResult = Validator.Validate(item);
                if (validationResult.Item1 is true)
                {
                    yield return item;
                    successfulItemsAmount++;
                }
            }

            this.Logger.LogInformation(string.Format(string.Format(SuccesInfoString, key, successfulItemsAmount)));
        }

        private async Task<string> GetContent(string url)
        {
            HttpResponseMessage response = await this.client.GetAsync(url);
            if (response.IsSuccessStatusCode is false)
            {
                this.Logger.LogError(string.Format(UrlAccessError, url));
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }

        private string CreateUrl(string searchKey)
        {
            var UrlSection = AppSettings.GetSection(AddressSegment);
            StringBuilder url = new StringBuilder();
            url.Append(UrlSection[AddressKey]);
            url.Append(searchKey);
            var ParametersSection = UrlSection.GetSection(AddressParametersKey);
            foreach (var param in ParametersSection.GetChildren())
            {
                url.Append(ParametersSeparator);
                url.Append(param.Key);
                url.Append(ParametersValueSign);
                url.Append(param.Value);
            }

            return url.ToString();
        }
    }
}