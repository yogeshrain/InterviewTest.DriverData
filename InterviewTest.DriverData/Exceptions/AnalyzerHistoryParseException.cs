using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    public class AnalyzerHistoryParseException : Exception
    {
        public AnalyzerHistoryParseException() : base()
        {
        }

        public AnalyzerHistoryParseException(string message) : base(message)
        {
        }

        public AnalyzerHistoryParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
