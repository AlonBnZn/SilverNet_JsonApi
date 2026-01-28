namespace NServiceBus.Service.Saga
{
    public class SubsriptionSagaData : ContainSagaData
    {
        public Guid JobGuid { get; set; }

        public int ProcessedMessages { get; set; }

        public int TotalMessages { get; set; }
    }
}
