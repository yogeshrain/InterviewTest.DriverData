﻿using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData.Extensions;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // Restricts class to assembly only, not accessible in functions/properties defined outside the assembly.
    internal class GetawayDriverAnalyser : IAnalyser
	{
        const int MINSPEED = 0,
                    MAXSPEED = 80;
        TimeSpan STARTTIME = new TimeSpan(13, 0, 0),
                 ENDTIME = new TimeSpan(14, 0, 0);
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
            if (history == null || history.Count() == 0)
                return this.DefaultAnalysis();
            var undocumented = new List<Period>();

            // Add missing undocumated periods as well as splice ends periods to honour working hours
            // Remove leading and trailing periods with zero speed
            var f = history.SpliceZeroSpeedPeriods().
                Filter(STARTTIME, ENDTIME, out undocumented).                 
                 Select(item =>
                 {
                     // Calculate rating based on rules
                     var duration = item.End - item.Start;
                     var rating = (item.AverageSpeed <= MAXSPEED && item.AverageSpeed > MINSPEED) ? decimal.Multiply(decimal.Divide(1, MAXSPEED), item.AverageSpeed) :
                                    item.AverageSpeed > MAXSPEED ? 1 : 0;

                     // Duration vehicle driven and multiplication with rating
                     return new
                     {
                         Duration = duration,
                         Total = decimal.Multiply(duration.Ticks, rating)
                     };
                 });

            if (f.Count() == 0)
                return this.DefaultAnalysis();

            // Compute weighted average and duration
            return this.ComputeHistoryAnalysis(f, undocumented);
        }
	}
}