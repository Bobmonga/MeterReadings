using System.Collections.Generic;
using MeterReadings.Database.Models;

namespace MeterReadings.Database
{
    public interface IRepository
    {
        List<Account> GetAccounts();

        List<MeterReading> GetMeterReadings();

        void SaveMeterReadings(List<MeterReading> meterReadings);
    }
}