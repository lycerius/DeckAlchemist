using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OpenScraping.Transformations;

namespace DeckAlchemist.WebApp.Api.Utilities
{
   public class LinkAttributeValue : ITransformationFromHtml
    {
    public object Transform(Dictionary<string, object> settings, HtmlAgilityPack.HtmlNode node, List<HtmlAgilityPack.HtmlNode> logicalParents)
    {
        if (node != null)
        {
            string attrName = "href";
            string fallBack = "#";
            if (settings != null && settings["_attrName"] != null && ((JValue)settings["_attrName"]).Type == JTokenType.String)
            {
                attrName = ((JValue)settings["_attrName"]).ToObject<string>();
            }
            if (settings != null && settings["_fallBack"] != null && ((JValue)settings["_fallBack"]).Type == JTokenType.String)
            {
                fallBack = ((JValue)settings["_fallBack"]).ToObject<string>();
            }

            var href = node.GetAttributeValue(attrName, fallBack);
            return href;
        }

        return null;
    }
}
}