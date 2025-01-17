﻿using MeterReadings.Database.Models;
using System.Collections.Generic;
using System.IO;

namespace MeterReadings.Business
{
    public interface IMeterReadingProcessor
    {
        MeterReadingResponse Process(Stream fileSteam);
    }
}