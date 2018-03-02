using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Sources.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
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
        public void AllCollectionsByUsersIds([FromBody]string[] users)
        {
          
        }

        //one or many cards
        [HttpPut("{cards}")]
        public void AddCardsToCollection([FromBody]string[] cardnames)
        {
         
        }

        //remove one ore many cards
        [HttpDelete("{cards}")]
        public void RemoveCardsFromCollection([FromBody]string[] cardnames)
        {
     
        }
    }
}
