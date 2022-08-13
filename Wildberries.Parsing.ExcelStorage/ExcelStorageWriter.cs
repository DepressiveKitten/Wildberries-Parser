using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Wildberries.Parsing.Interfaces;

namespace Wildberries.Parsing.ExcelStorage
{
    /// <inheritdoc/>
    public class ExcelStorageWriter : IStorageWriter
    {
        private static readonly string NoSheetExceptionMessage = "Create new sheet before saving objects";
        private static readonly string SaveDocumentMessage = "Document {0} was saved with {1} pages";
        private static readonly string OutputFileKeyName = "output-file";
        private static Tuple<int, string, Func<SiteItem, string>>[] columns = new Tuple<int, string, Func<SiteItem, string>>[]
        {
            new Tuple<int, string, Func<SiteItem, string>>(1, "Title", (SiteItem item) => item.Title),
            new Tuple<int, string, Func<SiteItem, string>>(2, "Brand", (SiteItem item) => item.Brand),
            new Tuple<int, string, Func<SiteItem, string>>(3, "Id", (SiteItem item) => item.Id.ToString()),
            new Tuple<int, string, Func<SiteItem, string>>(4, "Fedbacks", (SiteItem item) => item.Feedbacks.ToString()),
            new Tuple<int, string, Func<SiteItem, string>>(5, "Price", (SiteItem item) =>
            {
                var result = string.Format("{0:0.}", item.Price);
                return result.Remove(result.Length - 2);
            }),
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelStorageWriter"/> class.
        /// </summary>
        /// <param name="appSettings">App configuration.</param>
        /// <param name="logger">App logger.</param>
        public ExcelStorageWriter(IConfiguration appSettings, ILogger logger)
        {
            this.Package = new ExcelPackage();
            this.AppSettings = appSettings;
            this.Logger = logger;
        }

        private ExcelPackage Package { get; }

        private ExcelWorksheet Worksheet { get; set; }

        private int Row { get; set; }

        private IConfiguration AppSettings { get; }

        private ILogger Logger { get; }

        /// <inheritdoc/>
        public void CreatePage(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Worksheet = this.Package.Workbook.Worksheets.Add(name);
            this.Row = 1;
        }

        /// <inheritdoc/>
        public async Task Save()
        {
            await File.WriteAllBytesAsync(this.AppSettings[OutputFileKeyName], await this.Package.GetAsByteArrayAsync());

            this.Logger.LogInformation(string.Format(SaveDocumentMessage, this.AppSettings[OutputFileKeyName], this.Package.Workbook.Worksheets.Count));
        }

        /// <inheritdoc/>
        public async Task Write(IAsyncEnumerable<SiteItem> items)
        {
            if (this.Worksheet is null)
            {
                throw new InvalidOperationException(NoSheetExceptionMessage);
            }

            foreach (var column in columns)
            {
                this.Worksheet.Cells[this.Row, column.Item1].Value = column.Item2;
            }

            this.Row++;

            await foreach (var item in items)
            {
                foreach (var column in columns)
                {
                    this.Worksheet.Cells[this.Row, column.Item1].Value = column.Item3(item);
                }

                this.Row++;
            }
        }
    }
}
