using System;
using System.Threading;
using DeckAlchemist.Collector.Services;

namespace DeckAlchemist.Collector.Schedulers
{
    public class DeckDatabaseServiceScheduler : IDeckDatabaseServiceScheduler
    {
        readonly IDeckDatabaseUpdater deckDatabaseUpdater;
        Timer updateTimer;
        volatile bool inProcess;

        public DeckDatabaseServiceScheduler(IDeckDatabaseUpdater updater)
        {
            deckDatabaseUpdater = updater;
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
            if (!inProcess)
            {
                UpdateDatabase();
            }
        }

        void UpdateDatabase() 
        {
            try
            {
                inProcess = true;
                deckDatabaseUpdater.UpdateDecks();
                inProcess = false;   
            }
            catch(Exception e)
            {
                inProcess = false;
                throw e;
            }
        }

    }
}
