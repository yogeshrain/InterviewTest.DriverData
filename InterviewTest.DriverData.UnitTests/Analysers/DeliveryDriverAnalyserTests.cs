using System;
using InterviewTest.DriverData.Analysers;
using NUnit;
using NUnit.Framework;
using System.IO;
using InterviewTest.DriverData.Extensions;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
		[Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(7, 45, 0),
				DriverRating = 0.7638m
			};

			var actualResult = new DeliveryDriverAnalyser().Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}
        [Test]
        public void DeliveryExpectDefault()
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
        public void DeliveryExpectAnalyzerHistoryParseException()
        {
            Assert.Throws<AnalyzerHistoryParseException>(() =>
            {
                new DeliveryDriverAnalyser().Analyse(CannedDrivingData.GetPeriods("dummy.json"));
            });
        }

        [Test]
        public void DeliveryExpectCorrectUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 0.7638m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/canneddata.json"), true);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void DeliveryPenalizeCorrectUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 0.3819m
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/canneddata.json"), false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void DelieryRatin1CorrectUsingFile()
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
        public void DelieryNoPeriodsUsingFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            var actualResult = new DeliveryDriverAnalyser().Analyse(
                CannedDrivingData.GetPeriods(@"InterviewTest.DriverData.UnitTests/TestHistoryData/noperiods.json"), false);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
