using MeterReadings.Database.Models;
using System.Collections.Generic;

namespace MeterReadings.Business
{
    public interface IMeterReadingParser
    {
        List<MeterReading> Parse(List<string> rawReadings);
    }
}