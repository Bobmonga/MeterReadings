using System.Collections.Generic;
using System.IO;

namespace MeterReadings.Business.CSV
{
    public class CSVMeterReadingReader : IMeterReadingReader
    {
        public List<string> Read(Stream fileStream)
        {
            List<string> readings = new List<string>();

            using (var reader = new StreamReader(fileStream))
            {
                string headers = reader.ReadLine();
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
