using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    public class AnalyzerValidationException : Exception
    {
        public IList<Tuple<string, Period>> Errors { get; internal set; }

        public AnalyzerValidationException()
            : base()
        { }

        public AnalyzerValidationException(string message)
            : base(message)
        { }
    }
}
