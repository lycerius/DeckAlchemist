using System;

namespace DeckAlchemist.WebApp.Models
{
    //copyright code 2004 University of Alabama, Nicholas Revere.
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}