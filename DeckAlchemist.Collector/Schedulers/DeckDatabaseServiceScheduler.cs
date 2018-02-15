using System;
using System.Threading;
using DeckAlchemist.Collector.Services;

namespace DeckAlchemist.Collector.Schedulers
{
    public class DeckDatabaseServiceScheduler : IDeckDatabaseServiceScheduler, IDisposable
    {
        readonly IDeckDatabaseUpdater deckDatabaseUpdater;
        Timer updateTimer;
        volatile bool inProcess;
        const int hourTriggerDaily = 0;

        public DeckDatabaseServiceScheduler(IDeckDatabaseUpdater updater)
        {
            deckDatabaseUpdater = updater;
            updateTimer = new Timer((state) => _TimerTrigger(), null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            updateTimer.Change(0, Timeout.Infinite);
        }

        public void Stop()
        {
            Console.WriteLine("Stopped DeckScheduler");
            updateTimer.Dispose();
            while (inProcess) Thread.Yield();
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
                try
                {
                    UpdateDatabase(); 
                }
                catch(Exception e)
                {
                    //TODO: Log
                }
                var now = DateTimeOffset.UtcNow;
                var tomorrow = new DateTimeOffset(now.Year, now.Month, now.Day, hourTriggerDaily, 0, 0, 0, TimeSpan.FromMilliseconds(0)).AddDays(1);
                var delta = tomorrow.Subtract(now);
                updateTimer.Change(delta, Timeout.InfiniteTimeSpan);
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

        public void Dispose()
        {
            Stop();
        }
    }
}
