using MeterReadings.Business.CSV;
using MeterReadings.Database;
using MeterReadings.Database.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeterReadings.Test
{
    public class CSVParserTests
    {
        private CSVMeterReadingParser csvMeterReadingParser;

        [SetUp]
        public void Setup()
        {
            Account account = new Account
            {
                AccountId = 1234,
                FirstName = "Test",
                LastName = "Case"
            };

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(r => r.GetAccounts()).Returns(new List<Account> { account });
            csvMeterReadingParser = new CSVMeterReadingParser(mockRepo.Object);
        }

        [Test]
        public void MeterReadingSuccessfullyParsed()
        {
            List<string> rawReadings = new List<string>
            {
                "1234,22/04/2019 09:24,01002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().AccountId == 1234);
            Assert.IsTrue(meterReadings.First().MeterReadingDateTime == new DateTime(2019, 4, 22, 9, 24, 0));
            Assert.IsTrue(meterReadings.First().MeterReadingValue == 1002);
            Assert.IsTrue(meterReadings.First().ValidationFailure == null);
        }

        [Test]
        public void MeterReadingWithInvalidAccountIdFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "2344A,22/04/2019 09:24,01002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "Invalid AccountId 2344A");
        }

        [Test]
        public void MeterReadingWithNoMatchingAccountIdFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "1235,22/04/2019 09:24,01002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "No matching AccountId 1235");
        }

        [Test]
        public void MeterReadingWithInvalidMeterReadingDateTimeFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "1234,22/13/2019 09:24,01002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "Invalid MeterReadingDateTime 22/13/2019 09:24");
        }

        [Test]
        public void MeterReadingWithInvalidMeterReadingValueFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "1234,22/04/2019 09:24,B1002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "Invalid MeterReadingValue B1002");
        }

        [Test]
        public void MeterReadingWithHighMeterReadingValueFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "1234,22/04/2019 09:24,111002"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "MeterReadingValue 111002 too high");
        }

        [Test]
        public void MeterReadingWithLowhMeterReadingValueFailsValidation()
        {
            List<string> rawReadings = new List<string>
            {
                "1234,22/04/2019 09:24,-1"
            };

            List<MeterReading> meterReadings = csvMeterReadingParser.Parse(rawReadings);

            Assert.IsTrue(meterReadings.Count == 1);
            Assert.IsTrue(meterReadings.First().ValidationFailure == "MeterReadingValue -1 too low");
        }
    }
}