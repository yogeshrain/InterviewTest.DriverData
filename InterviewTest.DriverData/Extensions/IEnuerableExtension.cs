using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Extensions
{
    public static class IEnuerableExtension
    {
        public static IReadOnlyCollection<Period> Filter(this IReadOnlyCollection<Period> history,
            TimeSpan start, TimeSpan end, out List<Period> undocumented)
        {
            // Initialize Error and Undocumented Periods list
            var result = history.ToList();
            List<Tuple<string, Period>> errors = new List<Tuple<string, Period>>();
            undocumented = new List<Period>();

            // Enumerate to identify error and undocumeted periods
            var enumerator = result.OrderBy(item => item.Start).
                Where(instance => (start == TimeSpan.Zero && end == TimeSpan.Zero) || 
                    (instance.Start.TimeOfDay.IsInRange(instance.End.TimeOfDay, start, end))).GetEnumerator();

            enumerator.MoveNext();
            var previous = enumerator.Current;
            while (enumerator.MoveNext())
            {
                // Start time should be less than End time
                if (previous.Start > previous.End)
                {
                    errors.Add(Tuple.Create("Start time for a period should be less than End time", previous));
                }
                else
                {
                    // End time of previous period should be less than Start time of current period
                    var diff = enumerator.Current.Start - previous.End;
                    if (diff.Ticks < 0)
                    {
                        errors.Add(Tuple.Create("Period cannot start before end of previous period End", enumerator.Current));
                    }
                    else if (diff.Ticks > 0)
                    {

                        // If End time of previous does not match to Start time of Current, mark it as undocumented
                        undocumented.Add(new Period
                        {
                            Start = previous.End,
                            End = enumerator.Current.Start,
                            AverageSpeed = 0m
                        });
                    }
                }
                previous = enumerator.Current;
            }
            // Throw exception with errors list
            if (errors.Count() > 0)
                throw new AnalyzerValidationException("Invalid periods in history collection.")
                {
                    Errors = errors
                };
            result.AddRange(undocumented);

            return (TimeSpan.Zero == start && TimeSpan.Zero == end) ?
                result.OrderBy(item => item.Start).ToList().AsReadOnly() :
                result.OrderBy(item => item.Start).
                    Where(instance => (instance.Start.TimeOfDay.IsInRange(instance.End.TimeOfDay, start, end))).
                    Select(item =>
                    {
                        item.Start = item.Start.AddTicks(item.Start.TimeOfDay < start ? (start - item.Start.TimeOfDay).Ticks : 0);
                        item.End = item.End.AddTicks(item.End.TimeOfDay > end ? (end - item.End.TimeOfDay).Ticks : 0);
                        return item;
                    }).ToList().AsReadOnly();
        }

        public static IReadOnlyCollection<Period> SpliceZeroSpeedPeriods(this IReadOnlyCollection<Period> history)
        {
            return history.SkipWhile(item => item.AverageSpeed == 0).Reverse().
                SkipWhile(item => item.AverageSpeed == 0).Reverse().ToList().AsReadOnly();
        }
    }
}
