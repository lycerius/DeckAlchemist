using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DeckAlchemist.Collector.Objects.Messages;
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

        [HttpPost("trigger")]
        public IEnumerable<ServiceStatusMessage> TriggerServices([FromBody] IEnumerable<string> serviceNames)
        {
            var serviceTriggers = new List<ServiceStatusMessage>();
            if (serviceNames.Any())
            {
                foreach (var serviceName in serviceNames)
                {
                    try
                    {
                        switch (serviceName.ToLower())
                        {
                            case ("deck"):
                                try { deckService.Trigger(); serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.OK }); }
                                catch (OperationInProgressException oip)
                                {
                                    serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.IN_PROGRESS });
                                }
                                continue;
                            case ("card"):
                                try { cardService.Trigger(); serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.OK }); }
                                catch (OperationInProgressException oip)
                                {
                                    serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.IN_PROGRESS });
                                }
                                continue;
                            default:
                                serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.INVALID_NAME });
                                continue;
                        }
                    }
                    catch (Exception e)
                    {
                        serviceTriggers.Add(new ServiceStatusMessage { ServiceName = serviceName, ServiceStatus = ServiceStatusMessage.SERVER_ERROR });
                    }
                }
            }
            return serviceTriggers;
        }



        [HttpPost("trigger/all")]
        public IEnumerable<ServiceStatusMessage> TriggerAllServices()
        {
            var servicesTriggered = new List<ServiceStatusMessage>();

            //Card Service
            try
            {
                cardService.Trigger();
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "card", ServiceStatus = ServiceStatusMessage.OK });
            }
            catch (OperationInProgressException oip)
            {
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "card", ServiceStatus = ServiceStatusMessage.IN_PROGRESS });
            }
            catch (Exception e)
            {
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "card", ServiceStatus = ServiceStatusMessage.SERVER_ERROR });
            }

            //Deck Service
            try
            {
                deckService.Trigger();
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "deck", ServiceStatus = ServiceStatusMessage.OK });
            }
            catch (OperationInProgressException oip)
            {
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "deck", ServiceStatus = ServiceStatusMessage.IN_PROGRESS });
            }
            catch (Exception e)
            {
                servicesTriggered.Add(new ServiceStatusMessage() { ServiceName = "deck", ServiceStatus = ServiceStatusMessage.SERVER_ERROR });
            }

            return servicesTriggered;

        }
    }
}
