using System.IO;
using System.Threading.Tasks;

namespace MeterReadings.Business
{
    public interface IMeterReadingProcessor
    {
        Task<int> Process(Stream fileSteam);
    }
}