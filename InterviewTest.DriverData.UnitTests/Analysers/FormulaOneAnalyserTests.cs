using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using System.IO;
using InterviewTest.DriverData.Extensions;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
		[Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m
			};

            var actualResult = new FormulaOneAnalyser().Analyse(CannedDrivingData.History, true);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void FormulaOneExpectDefault()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.Zero,
                DriverRating = 0m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(null);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void FormulaOneExpectAnalyzerHistoryParseException()
        {
            Assert.Throws<AnalyzerHistoryParseException>(() =>
            {
                new DeliveryDriverAnalyser().Analyse(CannedDrivingData.GetPeriods("dummy.json"));
            });
        }

        [Test]
        public void FormulaOneExpectCorrectUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/canneddata.json"), true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void FormulaOnePenalizeCorrectUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/canneddata.json"), true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void FormulaOneRatin1CorrectUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.00m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/rating1.json"), false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void FormulaOneNoPeriodsUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods("InterviewTest.DriverData.UnitTests/TestHistoryData/noperiods.json"), false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void FormulaOneExpectExceptionInOverlappedPeriods()
        {
            Assert.Throws<AnalyzerValidationException>(() =>
            {
                new DeliveryDriverAnalyser().Analyse(CannedDrivingData.GetPeriods("InterviewTest.DriverData.UnitTests/TestHistoryData/overlappedperiods.json"), true);
            });
        }
    }
}
