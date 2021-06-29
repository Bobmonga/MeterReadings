using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadings.Business
{
    public interface IMeterReadingReader
    {
        Task<List<string>> Read(Stream fileStream);
    }
}