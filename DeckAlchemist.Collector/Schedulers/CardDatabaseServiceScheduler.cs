using System;
using System.Threading;
using DeckAlchemist.Collector.Services;

namespace DeckAlchemist.Collector.Schedulers
{
    public class CardDatabaseServiceScheduler : ICardDatabaseServiceScheduler
    {
        readonly ICardDatabaseUpdater cardDatabaseUpdater;
        Timer updateTimer;
        volatile bool inProcess;
        const int hourTriggerDaily = 0;

        public CardDatabaseServiceScheduler(ICardDatabaseUpdater updater)
        {
            cardDatabaseUpdater = updater;
            updateTimer = new Timer((state) => _TimerTrigger(), null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Trigger()
        {
            if (!inProcess)
                updateTimer.Change(0, Timeout.Infinite);
            else 
                throw new OperationInProgressException();
        }

        void _TimerTrigger() 
        {
            if(!inProcess)
            {
                UpdateDatabase();
            }
        }

        void UpdateDatabase()
        {
            try
            {
                inProcess = true;
                cardDatabaseUpdater.UpdateCardDatabase();
                inProcess = false;
            }
            catch (Exception e)
            {
                inProcess = false;
                throw e;
            }
        }

    }
}
