using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingReader : IMeterReadingReader
    {
        public async Task<List<string>> Read(Stream fileStream)
        {
            List<string> readings = new List<string>();

            using (var reader = new StreamReader(fileStream))
            {
                string reading;
                while ((reading = reader.ReadLine()) != null)
                {
                    readings.Add(reading);
                }
            }

            return readings;
        }
    }
}
