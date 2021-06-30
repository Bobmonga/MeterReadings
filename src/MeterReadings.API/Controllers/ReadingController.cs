using MeterReadings.Business;
using MeterReadings.Database;
using MeterReadings.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MeterReadings.API.Controllers
{
    [ApiController]
    [Route("meter-reading-uploads")]
    public class ReadingController : ControllerBase
    {
        private readonly ILogger<ReadingController> _logger;
        private readonly IMeterReadingProcessor _meterReadingProcessor;
        private readonly IRepository _repository;

        public ReadingController(ILogger<ReadingController> logger, IMeterReadingProcessor meterReadingProcessor, IRepository repository)
        {
            _logger = logger;
            _meterReadingProcessor = meterReadingProcessor;
            _repository = repository;
        }

        [HttpPost()]
        public IActionResult OnPostUpload(IFormFile meterReadingsFile)
        {
            try
            {
                MeterReadingResponse meterReadingResponse;

                using (Stream meterReadingsStream = meterReadingsFile.OpenReadStream())
                {
                    meterReadingResponse = _meterReadingProcessor.Process(meterReadingsStream);
                }

                return Ok(meterReadingResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "It's not worked!");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAccounts")]
        public IActionResult GetAccounts()
        {
            List<Account> accounts = _repository.GetAccounts();

            return new OkObjectResult(accounts);
        }

        [HttpGet("GetMeterReadings")]
        public IActionResult GetMeterReadings()
        {
            List<MeterReading> accounts = _repository.GetMeterReadings();

            return new OkObjectResult(accounts);
        }
    }
}
