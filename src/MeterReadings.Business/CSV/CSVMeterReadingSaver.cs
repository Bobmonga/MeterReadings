using MeterReadings.Database;
using MeterReadings.Database.Models;
using System.Collections.Generic;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingSaver : IMeterReadingSaver
    {
        private readonly IRepository _repository;

        public CSVMeterReadingSaver(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(List<MeterReading> meterReadings)
        {
            _repository.SaveMeterReadings(meterReadings);
        }
    }
}
