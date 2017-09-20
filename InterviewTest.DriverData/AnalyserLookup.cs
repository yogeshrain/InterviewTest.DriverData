using System;
using InterviewTest.DriverData.Analysers;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
		public static IAnalyser GetAnalyser(string type)
		{
            // Could be configuraed somewhere as possible analyzers 
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("friendly", "InterviewTest.DriverData.Analysers.FriendlyAnalyser");
            d.Add("delivery", "InterviewTest.DriverData.Analysers.DeliveryDriverAnalyser");
            d.Add("formulaone", "InterviewTest.DriverData.Analysers.FormulaOneAnalyser");
            d.Add("getaway", "InterviewTest.DriverData.Analysers.GetawayDriverAnalyser");

            string instance = string.Empty;
            if (!d.TryGetValue(type, out instance))
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");

            try
            {
                return (IAnalyser)Activator.CreateInstance(Type.GetType(instance));
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate analyzer:{instance}", ex);
            }
        }
	}
}
