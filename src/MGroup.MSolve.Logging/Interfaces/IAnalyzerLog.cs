﻿using System;
using MGroup.LinearAlgebra.Vectors;

namespace MGroup.MSolve.Logging.Interfaces
{
    public interface IAnalyzerLog
    {
        void StoreResults(DateTime startTime, DateTime endTime, IVectorView solution);
    }
}
