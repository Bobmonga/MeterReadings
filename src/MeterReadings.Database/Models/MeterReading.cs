using System;

namespace MeterReadings.Database.Models
{
    public class MeterReading
    {
        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadingValue { get; set; }

        public string ValidationFailure { get; set; }
    }
}
