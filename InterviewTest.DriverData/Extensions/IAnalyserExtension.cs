using InterviewTest.DriverData.Analysers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Extensions
{
    public static class IAnalyserExtension
    {
        public static HistoryAnalysis DefaultAnalysis(this IAnalyser analyser)
        {
            return new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.Zero,
                DriverRating = 0m
            };
        }
    }
}
