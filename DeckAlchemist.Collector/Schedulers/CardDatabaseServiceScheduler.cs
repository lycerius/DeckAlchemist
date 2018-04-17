using System;
using System.Threading;
using DeckAlchemist.Collector.Services;

namespace DeckAlchemist.Collector.Schedulers
{
    public class CardDatabaseServiceScheduler : ICardDatabaseServiceScheduler, IDisposable
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
            updateTimer.Change(0, Timeout.Infinite);
        }

        public void Stop()
        {
            Console.WriteLine("Stopped CardScheduler");
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
            if(!inProcess)
            {
                try
                {
                    UpdateDatabase();
                }
                catch
                {
                    
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
                cardDatabaseUpdater.UpdateCardDatabase();
                inProcess = false;
            }
            catch (Exception e)
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
