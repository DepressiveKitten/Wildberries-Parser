using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wildberries.Parsing.Interfaces;

namespace Wildberries.Parsing
{
    /// <inheritdoc/>
    public class WildberriesParsingService : IParsingService
    {
        private readonly string fileDontExistErrorMessage = "Input file with keys is misiing";
        private readonly string fileInfoMessage = "{0} Keys has been read from input file";
        private readonly string inputFileKeyName = "input-file";

        /// <summary>
        /// Initializes a new instance of the <see cref="WildberriesParsingService"/> class.
        /// </summary>
        /// <param name="appSettings">App configuration.</param>
        /// <param name="logger">App logger.</param>
        /// <param name="itemsReader">Reader of all items on site.</param>
        public WildberriesParsingService(IConfiguration appSettings, ILogger logger, IItemsReader itemsReader)
        {
            this.AppSettings = appSettings;
            this.Logger = logger;
            this.ItemsReader = itemsReader;
        }

        private IConfiguration AppSettings { get; }

        private ILogger Logger { get; }

        private IItemsReader ItemsReader { get; }

        /// <inheritdoc/>
        public async Task Parse()
        {
            List<string> searchKeys = await this.GetKeysFromFile();
            if (searchKeys is null)
            {
                return;
            }

            foreach (var searchKey in searchKeys)
            {
                var items = this.ItemsReader.Read(searchKey);

                // TODO: add saving to collection
                await foreach (var item in items)
                {
                }
            }
        }

        private async Task<List<string>> GetKeysFromFile()
        {
            if (!File.Exists(this.AppSettings[this.inputFileKeyName]))
            {
                this.Logger.LogError(this.fileDontExistErrorMessage);
                return null;
            }

            List<string> searchKeys = new List<string>();

            using (FileStream fileStream = new FileStream(this.AppSettings[this.inputFileKeyName], FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string searchKey;
                    while ((searchKey = await reader.ReadLineAsync()) != null)
                    {
                        searchKeys.Add(searchKey);
                    }
                }
            }

            this.Logger.LogInformation(string.Format(this.fileInfoMessage, searchKeys.Count));

            return searchKeys;
        }
    }
}
