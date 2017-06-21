using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
		// BONUS: What's the difference between DateTime and DateTimeOffset?
		public DateTimeOffset Start;
		public DateTimeOffset End;

		// BONUS: What's the difference between decimal and double?
		public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}
