using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirQuality.gRPC;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace server.Services
{
    public class AirQualityService : AirQuality.gRPC.AirQuality.AirQualityBase
    {
        public override async Task<AirQualityReport> GetReport(IAsyncStreamReader<Sample> requestStream, ServerCallContext context)
        {
            var samples = new List<(Timestamp sampleDate, double sampleValue)>();

            double high = 0;
            double low = Double.MaxValue;

            var report = new AirQualityReport();

            await foreach (var sample in requestStream.ReadAllAsync())
            {
                // only interested in Queens
                if (sample.CountyCode == 81)
                    samples.Add((sample.DateLocal, sample.ArithmeticMean));
            }

            foreach (var dailyData in samples.GroupBy(s => s.sampleDate))
            {
                var averageDailyReading = dailyData.Select(d => d.sampleValue).Average();

                if (averageDailyReading > high)
                    high = averageDailyReading;

                if (averageDailyReading < low)
                    low = averageDailyReading;

                report.Values.Add(new SampleValue() { SampleDate = dailyData.Key, Value = averageDailyReading });
            }

            report.High = high;
            report.Low = low;

            return report;
        }
    }
}
