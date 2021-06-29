using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingProcessor : IMeterReadingProcessor
    {
        private readonly IMeterReadingReader meterReadingReader;

        public CSVMeterReadingProcessor()
        {
            meterReadingReader = new CSVMeterReadingReader();
        }

        public async Task<int> Process(Stream fileStream)
        {
            List<string> readings = await meterReadingReader.Read(fileStream);

            return readings.Count;
        }
    }
}
