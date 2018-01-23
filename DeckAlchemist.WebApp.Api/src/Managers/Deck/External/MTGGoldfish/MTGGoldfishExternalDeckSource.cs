using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DeckAlchemist.WebApp.Api.Objects.Deck;
using DeckAlchemist.WebApp.Api.Objects.Deck.MTG;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;

namespace DeckAlchemist.WebApp.Api.Managers.Deck.External.MTGGoldfish
{
    public class MTGGoldfishExternalDeckSource : IExternalDeckSource
    {
        private const string MTGGoldfishEndpoint = "https://www.mtggoldfish.com";

        private const string StandardLinksParsingConfig = @"
            {
                ""decklinks"": {
                    ""_xpath"": ""//a[@class='card-image-tile-link-overlay']"",
                    ""_transformations"": [
                    {
                      ""_type"": ""LinkAttributeValue"",
                      ""_attrName"": ""href"",
                      ""_fallBack"": ""#""
                    }
                  ]
                }
            }
        ";

        private const string DeckPageParsingConfig = @"
        {
            ""deck"": {
                ""name"": ""//h2[@class='deck-view-title']/text()[1]"",
                ""meta"": ""//div[@class='container-fluid layout-container-fluid']/p""
            }
        }
        ";
        public IDeck GetAllDecks()
        {
            var links = GetAllDeckLinks();
            var decks = GetDecksFromLinks(links);
            return null;
        }

        private IEnumerable<IDeck> GetDecksFromLinks(IEnumerable<string> links)
        {
            foreach(var link in links)
            {
                var formattedLink = $"{MTGGoldfishEndpoint}{link}#paper";
                GetDeckFromLink(formattedLink);
            }
            return null;
        }

        private IDeck GetDeckFromLink(string link)
        {
            var source = DownloadWebpageSource(link);
                var scraper = CreateScraper(DeckPageParsingConfig);
                var deck = scraper.Extract(source);
                return new MTGDeck {
                   Name = (string) deck.First["name"]
                   //TODO: COntinue, Error checking, Cleanup
                };
        }

        private IEnumerable<string> GetAllDeckLinks()
        {
            var endpoint = $"{MTGGoldfishEndpoint}/metagame/standard/full#paper";
            var source = DownloadWebpageSource(endpoint);
            var scraper = CreateScraper(StandardLinksParsingConfig);
            var results = scraper.Extract(source);
            var links = GetDeckLinks(results);
            return links;   
        }

        private string DownloadWebpageSource(string url)
        {
            var client = NewHttpClient();
            var download = client.GetStringAsync(url);
            download.Wait();
            return download.Result;
        }

        private IEnumerable<string> GetDeckLinks(JContainer extraction)
        {
            var root = extraction.First.First as JArray;
            var results = new List<string>();

            return root.Select(token => (string) token);
        }

        private StructuredDataExtractor CreateScraper(string configString)
        {
            var config = StructuredDataConfig.ParseJsonString(configString);
            var scraper = new StructuredDataExtractor(config);
            return scraper;
        }
        private HttpClient NewHttpClient() {
            return new HttpClient();
        }
    }
}