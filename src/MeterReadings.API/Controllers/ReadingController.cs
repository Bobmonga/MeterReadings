using MeterReadings.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadings.API.Controllers
{
    [ApiController]
    [Route("meter-reading-uploads")]
    public class ReadingController : ControllerBase
    {
        private readonly ILogger<ReadingController> _logger;
        private readonly IMeterReadingProcessor _meterReadingProcessor;

        public ReadingController(ILogger<ReadingController> logger, IMeterReadingProcessor meterReadingProcessor)
        {
            _logger = logger;
            _meterReadingProcessor = meterReadingProcessor;
        }

        [HttpPost()]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile meterReadingsFile)
        {
            int lineCount;

            using (Stream meterReadingsStream = meterReadingsFile.OpenReadStream())
            {
                lineCount = await _meterReadingProcessor.Process(meterReadingsStream);
            }

            return Ok(lineCount);
        }
    }
}
