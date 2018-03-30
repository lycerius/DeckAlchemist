using System;
using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.Api.Sources.Collection;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeckAlchemist.Api.Contracts;
using DeckAlchemist.Support.Objects.Collection;
using System.Net.Http;
using DeckAlchemist.Api.Utility;
using Newtonsoft.Json;
using DeckAlchemist.Support.Objects.Cards;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/collection")]
    public class CollectionsController : Controller
    {
        readonly ICollectionSource _collectionSource;
        readonly IMtgCardSource _cardSource;
        readonly IUserSource _userSource;

        public CollectionsController(ICollectionSource collectionSource, IMtgCardSource cardSource, IUserSource userSource)
        {
            _collectionSource = collectionSource;
            _cardSource = cardSource;
            _userSource = userSource;
        }
        [HttpGet]
        public CollectionModel GetCollection()
        {
            var uId = HttpContext.User.Id();
            var result = _collectionSource.GetCollection(uId);
            if (result == null) return null;
            var uniqueCardNames = GetUniqueCardNames(result.OwnedCards.Keys, result.BorrowedCards.Keys);
            var cardInfo = GetCardInfo(uniqueCardNames);
            var model = new CollectionModel
            {
                CardInfo = cardInfo,
                UserCollection = result
            };
            return model;  
        } 

        [HttpGet("slim")]
        public CollectionModel GetCollectionSlim() 
        {
            var uId = HttpContext.User.Id();
            var result = _collectionSource.GetCollection(uId);
            if (result == null) return null;
            var model = new CollectionModel
            {
                UserCollection = result
            };
            return model;
        }

        IEnumerable<string> GetUniqueCardNames(IEnumerable<string> owned, IEnumerable<string> borrowed)
        {
            var cardNames = new HashSet<string>();
            if(owned != null) foreach (var card in owned)
                cardNames.Add(card);
            if(borrowed != null) foreach (var card in borrowed)
                cardNames.Add(card);

            return cardNames;
        }

        IDictionary<string, IMtgCard> GetCardInfo(IEnumerable<string> cardNames) 
        {
            var cardsResult = _cardSource.GetCardsByNames(cardNames.ToArray());
            var index = new Dictionary<string, IMtgCard>();
            foreach(var card in cardsResult) 
            {
                index.Add(card.Name, card);    
            }
            return index;
        }

        //add one or many cards
        [HttpPut("cards")]
        public IActionResult AddCardsToCollection([FromBody]IList<string> cardnames)
        {
            try
            {
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                var result = _collectionSource.AddCardToCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch(Exception){
                return StatusCode(500);
            }
        }

        //remove one or many cards
        [HttpDelete("cards")]
        public IActionResult RemoveCardsFromCollection([FromBody]string[] cardnames)
        {
            try
            {
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var cardExists = _cardSource.CheckExistance(cardnames);
                if (!cardExists) return StatusCode(401);
                var result = _collectionSource.RemoveCardFromCollection(uId, cardnames);
                if (result) return StatusCode(200);
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        //lend one ore menay cards
        [HttpPost("lend")]
        public IActionResult LendcardsTo([FromBody] LendContract lendContract){
            try
            {
                var uId = Utility.UserInfo.Id(HttpContext.User);
                var userEmail = Utility.UserInfo.Email(HttpContext.User);
                var reciverExists = _userSource.UserExists(lendContract.Lender);
                if (!reciverExists) return StatusCode(401);
                var markAsLent = _collectionSource.MarkCardAsLent(uId, lendContract.Lendee, lendContract.CardsAndAmounts);
                var uIdOfRevicer = lendContract.Lendee;
                var reciveCard = _collectionSource.AddCardAsLent(uId, lendContract.Lendee, lendContract.CardsAndAmounts);
                if (markAsLent && reciveCard) return StatusCode(200);
                return StatusCode(500);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("csv")]
        public async Task<IActionResult> AddCardsFromCsv()
        {
            var csv = Request.Form.Files.FirstOrDefault();
            var tempFile = CreateTempFileAndAcceptUpload(csv.OpenReadStream());
            var uId = HttpContext.User.Id();
            using(var reader = new StreamReader(new FileStream(tempFile, FileMode.Open)))
            {
                using(var csvReader = new CsvHelper.CsvReader(reader))
                {
                    var cardEntry = new 
                    {
                        CardName = default(string),
                        Amount = default(int)
                    };
                    var entries = csvReader.GetRecords(cardEntry);
                    var toDict = new Dictionary<string, int>(entries.Select(entry => new KeyValuePair<string, int>(entry.CardName, entry.Amount)));
                    System.IO.File.Delete(tempFile);
                    _collectionSource.AddCardToCollection(uId, toDict);
                    return StatusCode(200);

                }
            }

        }

        string CreateTempFileAndAcceptUpload(Stream upload)
        {
            var tempFilePath = Path.GetTempFileName();

            using (var reader = new StreamReader(upload))
            {

                using (var fileStream = new FileStream(tempFilePath, FileMode.OpenOrCreate))
                {
                    var line = "";
                    using (var writer = new StreamWriter(fileStream))
                    {
                        while ((line = reader.ReadLine()) != null)
                            writer.WriteLine(line);
                    }

                }
            }

            return tempFilePath;
        }
    }
}
