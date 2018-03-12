using System;
namespace DeckAlchemist.Collector.Schedulers
{
    public interface IDeckDatabaseServiceScheduler
    {
        void Start();
        void Stop();
        void Trigger();
    }
}
