using System.Collections.Generic;

namespace InterviewTest.DriverData.Analysers
{
	public interface IAnalyser
	{
		HistoryAnalysis Analyse(IReadOnlyCollection<Period> history);
	}
}
