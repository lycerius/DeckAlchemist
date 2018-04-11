namespace DeckAlchemist.Collector.Objects.Messages
{
    public class ServiceStatusMessage
    {
        public const string OK = "ok";
        public const string INVALID_NAME = "invalid-name";
        public const string IN_PROGRESS = "in-progress";
        public const string SERVER_ERROR = "error";
        public string ServiceName { get; set; }
        public string ServiceStatus { get; set; }
    }
}
