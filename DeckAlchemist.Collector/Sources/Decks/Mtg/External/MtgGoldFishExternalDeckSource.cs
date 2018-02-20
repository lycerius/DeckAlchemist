using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using DeckAlchemist.Api.Objects.Deck;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;

namespace DeckAlchemist.Collector.Sources.Decks.Mtg.External
{
    public class MtgGoldFishExternalDeckSource : IMtgExternalDeckSource
    {
        const string MTGGoldfishEndpoint = "https://www.mtggoldfish.com";

        public IEnumerable<IMtgDeck> GetAllDecks()
        {
            return GetDecksFromLinks(GetAllDeckLinks(), true).ToList();
        }

        private IEnumerable<IMtgDeck> GetDecksFromLinks(IEnumerable<string> links, bool alsoCards)
        {
            return links.Select(link => GetDeckFromLink($"{MTGGoldfishEndpoint}{link}#paper", alsoCards));
        }

        private IMtgDeck GetDeckFromLink(string link, bool alsoCards)
        {
            var deck = (alsoCards ? DeckWithCardsScraper : DeckWithoutCardsScraper).Extract(DownloadWebpageSource(link)).First.First;
            return new MtgDeck
            {
                Name = (string)deck["name"],
                Meta = ParseMetaFromText((string)deck["meta"]),
                Cards = alsoCards ? ParseCardsFromScrapeResult((JArray)deck["cards"]) : null
            };
        }

        private IDictionary<string, IMtgDeckCard> ParseCardsFromScrapeResult(JArray jCards)
        {
            var conversion = new Dictionary<string, IMtgDeckCard>();

            var deckCards = jCards.Select(jCard => new MtgDeckCard
            {
                Count = (int)jCard["count"],
                Name = (string)jCard["name"]
            });

            //TODO: Issues with sideboard, right now I think the logic is that cards located in the sideboard is essentially part of the whole deck construction,
            //So if cards of the same type exist in the sideboard, just add to the count for that card type
            foreach (var card in deckCards)
            {
                if (conversion.ContainsKey(card.Name))
                    conversion[card.Name].Count += card.Count;
                else
                    conversion[card.Name] = card;
            }


            return conversion;
        }

        private double ParseMetaFromText(string text)
        {
            var result = PercentageMatchingPattern.Match(text);

            if (result.Success)
                return double.Parse(result.Groups.Last().Value);

            return 0D;
        }

        private IEnumerable<string> GetAllDeckLinks()
        {
            var endpoint = $"{MTGGoldfishEndpoint}/metagame/standard/full#paper";
            var results = DeckLinksScraper.Extract(DownloadWebpageSource(endpoint));
            return GetDeckLinksFromScraper(results);
        }

        private IEnumerable<string> GetDeckLinksFromScraper(JContainer extraction)
        {
            return extraction.First.First.Select(token => (string)token);
        }

        private string DownloadWebpageSource(string url)
        {
            var client = new HttpClient();
            var download = client.GetStringAsync(url);
            download.Wait();
            return download.Result;
        }

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

        private const string DeckWithCardsPageParsingConfig = @"
        {
            ""deck"": {
                ""name"": ""//h2[@class='deck-view-title']/text()[1]"",
                ""meta"": ""//div[@class='container-fluid layout-container-fluid']/p"",
                ""cards"": {
                    ""_xpath"": ""//div[@id='tab-paper']//*//table[@class='deck-view-deck-table']//tr[.//td[@class='deck-col-qty']]"",
                    ""count"": "".//td[@class='deck-col-qty']"",
                    ""name"": "".//td[@class='deck-col-card']/a""
                }    
            }
        }
        ";

        private const string DeckWithoutCardsPageParsingConfig = @"
        {
            ""deck"": {
                ""name"": ""//h2[@class='deck-view-title']/text()[1]"",
                ""meta"": ""//div[@class='container-fluid layout-container-fluid']/p"",   
            }
        }
        ";

        private readonly StructuredDataExtractor DeckWithCardsScraper = new StructuredDataExtractor(StructuredDataConfig.ParseJsonString(DeckWithCardsPageParsingConfig));

        private readonly StructuredDataExtractor DeckWithoutCardsScraper = new StructuredDataExtractor(StructuredDataConfig.ParseJsonString(DeckWithoutCardsPageParsingConfig));

        private readonly StructuredDataExtractor DeckLinksScraper = new StructuredDataExtractor(StructuredDataConfig.ParseJsonString(StandardLinksParsingConfig));

        private readonly Regex PercentageMatchingPattern = new Regex("((\\d+(?:\\.\\d+))%)");
    }
}
