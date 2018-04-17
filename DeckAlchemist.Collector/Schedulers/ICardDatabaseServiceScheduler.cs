namespace DeckAlchemist.Collector.Schedulers
{
    public interface ICardDatabaseServiceScheduler
    {
        void Start();
        void Stop();
        void Trigger();
    }
}
