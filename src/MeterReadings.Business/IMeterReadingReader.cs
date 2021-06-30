using System.Collections.Generic;
using System.IO;

namespace MeterReadings.Business
{
    public interface IMeterReadingReader
    {
        List<string> Read(Stream fileStream);
    }
}