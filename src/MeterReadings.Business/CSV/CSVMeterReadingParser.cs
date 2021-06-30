using MeterReadings.Database;
using MeterReadings.Database.Models;
using System;
using System.Collections.Generic;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingParser : IMeterReadingParser
    {
        private readonly IRepository _repository;

        public CSVMeterReadingParser(IRepository repository)
        {
            _repository = repository;
        }

        public List<MeterReading> Parse(List<string> rawReadings)
        {
            //TODO split into seperate responsibilities.
            List<Account> accounts = _repository.GetAccounts();
            List<MeterReading> meterReadings = new List<MeterReading>();

            foreach (string reading in rawReadings)
            {
                var readingParts = reading.Split(",");

                MeterReading meterReading = new MeterReading();

                if (!int.TryParse(readingParts[0], out int accountId))
                {
                    meterReading.ValidationFailure = $"Invalid AccountId {readingParts[0]}";
                    meterReadings.Add(meterReading);
                    continue;
                }

                meterReading.AccountId = accountId;

                if (!accounts.Exists(a => a.AccountId == accountId))
                {
                    meterReading.ValidationFailure = $"No matching AccountId {accountId}";
                    meterReadings.Add(meterReading);
                    continue;
                }

                if (!DateTime.TryParse(readingParts[1], out DateTime meterReadingDateTime))
                {
                    meterReading.ValidationFailure = $"Invalid MeterReadingDateTime{readingParts[1]}";
                    meterReadings.Add(meterReading);
                    continue;
                }

                meterReading.MeterReadingDateTime = meterReadingDateTime;

                if (!int.TryParse(readingParts[2], out int meterReadingValue))
                {
                    meterReading.ValidationFailure = $"Invalid MeterReadingValue {readingParts[2]}";
                    meterReadings.Add(meterReading);
                    continue;
                }

                if (meterReadingValue > 99999)
                {
                    meterReading.ValidationFailure = $"MeterReadingValue {meterReadingValue} too high";
                    meterReadings.Add(meterReading);
                    continue;
                }

                if (meterReadingValue < 0)
                {
                    meterReading.ValidationFailure = $"MeterReadingValue {meterReadingValue} too low";
                    meterReadings.Add(meterReading);
                    continue;
                }

                meterReading.MeterReadingValue = meterReadingValue;

                meterReadings.Add(meterReading);
            }

            return meterReadings;
        }
    }
}
