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

        public static HistoryAnalysis ComputeHistoryAnalysis(this IAnalyser analyser, IEnumerable<dynamic> result, 
            bool hasUndocumentedPeriods)
        {
            // Compute weighted average and duration
            return new HistoryAnalysis
            {
                // If driver finishes ride early than designated time, total duration needs to be computed
                AnalysedDuration = TimeSpan.FromTicks(result.Sum(item => (long)item.Duration.Ticks)),
                DriverRating = decimal.Divide(decimal.Divide(result.Sum(item => (decimal)item.Total), result.Sum(item => (long)item.Duration.Ticks)),
                    hasUndocumentedPeriods ? 2 : 1)
            };
        }

        public static HistoryAnalysis Analyse(this IAnalyser analyser, IReadOnlyCollection<Period> history,
            bool bypassPenalty)
        {
            // Note: Assumption here is existing analysers implementation should not be amended
            // It's actually going one step in reverse and some additional computation
            var result = analyser.Analyse(history);

            if (bypassPenalty)
            {
                List<Period> undocumented = null;

                // Assumption is no Stat and End time mentioned
                history.Filter(TimeSpan.Zero, TimeSpan.Zero, out undocumented);

                // Double the rating
                result.DriverRating = decimal.Multiply(result.DriverRating, undocumented.Count() > 0 ? 2 : 1);
            }

            return result;
        }
    }
}
