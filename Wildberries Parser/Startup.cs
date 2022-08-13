using System;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wildberries.Parsing;
using Wildberries.Parsing.ExcelStorage;
using Wildberries.Parsing.Interfaces;
using Wildberries.Parsing.JsonSearchPage;
using Wildberries.Parsing.JsonSearchPage.Deserialization;
using Wildberries.Parsing.JsonSearchPage.Interfaces;

namespace Wildberries_Parser
{
    /// <summary>
    /// Resolves dependencies and configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets project configuration.
        /// </summary>
        public IConfiguration AppSettings { get; } = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        /// <summary>
        /// Get ParsingService instance.
        /// </summary>
        /// <returns>ParsingService instance.</returns>
        public IParsingService CreateParsingService()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Trace);
            });

            IServiceProvider services = new ServiceCollection()
                .AddSingleton<ILogger>(loggerFactory.CreateLogger<WildberriesParsingService>())
                .AddSingleton(this.AppSettings)
                .AddSingleton<IParsingService, WildberriesParsingService>()
                .AddSingleton<IItemsReader, SearchPageJsonReader>()
                .AddSingleton<ISiteItemValidator, ValidatorStub>()
                .AddSingleton<IJsonParser, JsonParser>()
                .AddSingleton<IStorageWriter, ExcelStorageWriter>()
                .AddSingleton((asd) =>
                new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                }).CreateMapper())
                .BuildServiceProvider();

            return (IParsingService)services.GetService(typeof(IParsingService));
        }
    }
}