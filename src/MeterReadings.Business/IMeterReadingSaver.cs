using MeterReadings.Database.Models;
using System.Collections.Generic;

namespace MeterReadings.Business
{
    public interface IMeterReadingSaver
    {
        void Save(List<MeterReading> meterReadings);
    }
}