using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DeckAlchemist.Collector.Schedulers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeckAlchemist.Collector.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        readonly IDeckDatabaseServiceScheduler deckService;
        readonly ICardDatabaseServiceScheduler cardService;

        public ServiceController(IDeckDatabaseServiceScheduler deckDatabaseService, ICardDatabaseServiceScheduler cardDatabaseService)
        {
            deckService = deckDatabaseService;
            cardService = cardDatabaseService;
        }

        [HttpPost("all/now")]
        public IDictionary<string, string> TriggerAllServices() {
            var servicesTriggered = new Dictionary<string, string>();

            //Card Service
            try
            {
                cardService.Trigger();
                servicesTriggered["cards"] = "ok";
            }
            catch (OperationInProgressException oip)
            {
                servicesTriggered["cards"] = "in-progress";
            }            
            catch(Exception e)
            {
                servicesTriggered["cards"] = "error";
            }

            //Deck Service
            try
            {
                deckService.Trigger();
                servicesTriggered["decks"] = "ok";
            }
            catch(OperationInProgressException oip)
            {
                servicesTriggered["decks"] = "in-progress";
            }
            catch(Exception e)
            {
                servicesTriggered["decks"] = "error";
            }

            return servicesTriggered;

        }

        [HttpPost("decks/now")]
        public IActionResult TriggerDeckServiceNow() {
            try
            {
                deckService.Trigger();
                return StatusCode(200);
            }
            catch(OperationInProgressException oip)
            {
                //We don't want to trigger the service if it is already in progress
                return StatusCode(503);
            }
            catch(Exception e)
            {
                //TODO: Log Exception
                return StatusCode(500);
            }

        }

        [HttpPost("cards/now")]
        public IActionResult TriggerCardServiceNow() {
            try
            {
                cardService.Trigger();
                return StatusCode(200);
            }
            catch (OperationInProgressException oip)
            {
                //We don't want to trigger the service if it is already in progress
                return StatusCode(503);
            }
            catch (Exception e)
            {
                //TODO: Log Exception
                return StatusCode(500);
            }
        }
    }
}
