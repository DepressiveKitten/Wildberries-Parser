using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Wildberries.Parsing.JsonSearchPage.Interfaces;

namespace Wildberries.Parsing.JsonSearchPage.Deserialization
{
    /// <inheritdoc/>
    public class JsonParser : IJsonParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser"/> class.
        /// </summary>
        /// <param name="mapper">Mapper from product to SiteItem.</param>
        public JsonParser(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        private IMapper Mapper { get; }

        /// <inheritdoc/>
        public IEnumerable<SiteItem> Parse(string input)
        {
            JsonSearchPageDocument document = JsonSerializer.Deserialize<JsonSearchPageDocument>(input);

            return from item in document.PageData.Products
                   select this.Mapper.Map<Product,SiteItem>(item);
        }
    }
}
