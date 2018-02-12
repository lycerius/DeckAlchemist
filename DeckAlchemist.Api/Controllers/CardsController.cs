using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckAlchemist.Api.Objects.Cards.Mtg;
using DeckAlchemist.Api.Sources.Cards.Mtg;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Api.Controllers
{
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        readonly IMTGCardSource _source;

        public CardsController(IMTGCardSource source)
        {
            _source = source;
        }

        [HttpPost("names")]
        public IEnumerable<IMtgCard> GetCardsByName([FromBody] string[] names)
        {
            return (IEnumerable < IMtgCard >)_source.GetCardsByNames(names).Select(card => MtgCard.FromMtg(card));
        }

    }
}
