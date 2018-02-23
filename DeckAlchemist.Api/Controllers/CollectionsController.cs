using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Objects.Collection;
using DeckAlchemist.Api.Sources.Collection;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Route("api/collection")]
    public class CollectionsController : Controller
    {
        readonly ICollectionSource _source;

        public CollectionsController(ICollectionSource source)
        {
            _source = source;
        }


        //one or many users
        [HttpGet]
        public List<string> AllCollectionsByUsersIds([FromBody]string[] users)
        {
            var collectionIds = new List<string>();
            foreach( var user in users){
                collectionIds.Add(_source.GetCollectionIdOf(user));
            }
            return collectionIds;
        }

        //one or many cards
        [HttpPut("{cards}")]
        public List<string> AddCardsToCollection([FromBody]string[] cardnames, [FromBody]string userid )
        {
            var errorOnAdd = new List<string>();
            foreach(var card in cardnames){
                if (!_source.AddCard(card,userid)){
                    errorOnAdd.Add(card);
                }
            }
            return errorOnAdd;
        }

        [HttpDelete("{cards}")]
        public List<string> RemoveCardsFromCollection([FromBody]string[] cardnames, [FromBody]string userid)
        {
            var errorOnDel = new List<string>();
            foreach (var card in cardnames)
            {
                if (!_source.DeleteCard(card, userid))
                {
                    errorOnDel.Add(card);
                }
            }
            return errorOnDel;
        }
    }
}
