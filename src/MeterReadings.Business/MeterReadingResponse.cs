using MeterReadings.Database.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeterReadings.Business
{
    public class MeterReadingResponse
    {
        [JsonProperty(Order = 1)]
        public int SuccessfulReadings { get; set; }

        [JsonProperty(Order = 2)]
        public int FailedReadings { get; set; }

        [JsonProperty(Order = 3)]
        public List<MeterReading> AllTheReadings { get; set; }
    }
}
