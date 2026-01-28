namespace NServiceBus.Service.ValueObjects
{
    public class BillingCycle
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public BillingCycle()
        {

        }

        public BillingCycle(int year, int month)
        {
            Year = year;

            Month = month;
        }
    }
}
