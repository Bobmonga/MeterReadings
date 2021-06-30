using MeterReadings.Database.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingProcessor : IMeterReadingProcessor
    {
        private readonly IMeterReadingReader _meterReadingReader;
        private readonly IMeterReadingParser _meterReadingParser;
        private readonly IMeterReadingSaver _meterReadingSaver;

        public CSVMeterReadingProcessor(IMeterReadingReader meterReadingReader, IMeterReadingParser meterReadingParser, IMeterReadingSaver meterReadingSaver)
        {
            _meterReadingReader = meterReadingReader;
            _meterReadingParser = meterReadingParser;
            _meterReadingSaver = meterReadingSaver;
        }

        public MeterReadingResponse Process(Stream fileStream)
        {
            List<string> rawReadings = _meterReadingReader.Read(fileStream);

            List<MeterReading> meterReadings = _meterReadingParser.Parse(rawReadings);

            _meterReadingSaver.Save(meterReadings);

            MeterReadingResponse meterReadingResponse = new MeterReadingResponse();
            meterReadingResponse.AllTheReadings = meterReadings;
            meterReadingResponse.SuccessfulReadings = meterReadings.Where(m => string.IsNullOrWhiteSpace(m.ValidationFailure)).Count();
            meterReadingResponse.FailedReadings = meterReadings.Where(m => !string.IsNullOrWhiteSpace(m.ValidationFailure)).Count();

            return meterReadingResponse;
        }
    }
}
