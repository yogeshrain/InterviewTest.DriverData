using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Extensions;

namespace InterviewTest.Commands
{
	public class AnalyseHistoryCommand
	{
		// BONUS: What's great about readonly?
        // Value can be assigned at declaration or in constructor only, since analyser lookup does the 
        // job of assignment - one cannot change analyser once it is instantiated.
		private readonly IAnalyser _analyser;

        //Additional command option
        public bool BypassPenlty { get; set; }

        public string DataSourcePath { get; set; }

		public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
		{
            var analysisType = arguments.Count() > 1 ? arguments.First() : arguments.Single();
            bool bypass;
            bool.TryParse(arguments.ElementAt(1), out bypass); // Never mind exception here
            BypassPenlty = bypass;
            DataSourcePath = arguments.ElementAt(2) ?? string.Empty;
            _analyser = AnalyserLookup.GetAnalyser(analysisType);
		}

		public void Execute()
		{
            var analysis = _analyser.Analyse(string.IsNullOrEmpty(DataSourcePath) ? CannedDrivingData.History :
                CannedDrivingData.GetPeriods(DataSourcePath), BypassPenlty);

			Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
			Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
		}
	}
}
