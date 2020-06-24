using System;
using System.Net.Http;
using System.Threading.Tasks;
using static WorldTime.gRPC.WorldTime;
using WorldTime.gRPC;
using Grpc.Net.Client;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using AirQuality.gRPC;
using static AirQuality.gRPC.AirQuality;
using Google.Protobuf.WellKnownTypes;
using GrpcGreeterClient;

namespace client
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:8001");

            var so2data = await File.ReadAllTextAsync("data//SO2-NY.json");

            var parsed = JsonConvert.DeserializeObject<SampleData>(so2data);

            var client = new AirQualityClient(channel);

            using (var call = client.GetReport())
            {
                foreach (var sample in parsed.Data.Select(ToSample))
                {
                    await call.RequestStream.WriteAsync(sample);
                }

                await call.RequestStream.CompleteAsync();

                var response = await call;

                Console.WriteLine($"High: {response.High}, Low: {response.Low}");
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        
        static Sample ToSample(AirQualitySample qual)
        {
            return new Sample()
            {
                StateCode = qual.StateCode,
                CountyCode = qual.CountyCode,
                SiteNumber = qual.SiteNumber,
                ParameterCode = qual.ParameterCode,
                Poc = qual.Poc,
                Latitude = qual.Latitude,
                Longitude = qual.Longitude,
                Datum = qual.Datum,
                Parameter = qual.Parameter,
                SampleDuration = qual.SampleDuration,
                DateLocal = Timestamp.FromDateTime(DateTime.SpecifyKind(qual.DateLocal, DateTimeKind.Utc)),
                UnitsOfMeasure = qual.UnitsOfMeasure,
                EventType = qual.EventType,
                ObservationCount = qual.ObservationCount,
                ObservationPercent = qual.ObservationPercent,
                ValidityIndicator = qual.ValidityIndicator,
                ArithmeticMean = qual.ArithmeticMean,
                FirstMaxValue = qual.FirstMaxValue,
                FirstMaxHour = qual.FirstMaxHour,
                MethodCode = qual.MethodCode,
                Method = qual.Method,
                LocalSiteName = qual.LocalSiteName,
                SiteAddress = qual.SiteAddress,
                State = qual.State,
                County = qual.County,
                City = qual.City,
                CbsaCode = qual.CbsaCode,
                Cbsa = qual.Cbsa,
                DateOfLastChange = Timestamp.FromDateTime(DateTime.SpecifyKind(qual.DateOfLastChange, DateTimeKind.Utc)),
            };
        }
    }
}
