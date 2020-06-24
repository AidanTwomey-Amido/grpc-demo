using System;
using System.Threading.Tasks;
using Grpc.Core;
using WorldTime.Calculations;
using WorldTime.gRPC;

namespace server.Services
{
    public class WorldTimeService : WorldTime.gRPC.WorldTime.WorldTimeBase
    {
        private readonly ITimeCalculator timeCalculator;
        
        public WorldTimeService(ITimeCalculator timeCalculator)
        {
            this.timeCalculator = timeCalculator;
        }

        public override Task<WorldTimeReply> GetTime(WorldTime.gRPC.Point request, ServerCallContext context)
        {
            int offset = timeCalculator.GetOffset(MapPoint(request));

            return Task.FromResult(new WorldTimeReply
            {
                Localtime = DateTime.Now.AddHours(offset).ToShortTimeString()
            });
        }

        private static WorldTime.Calculations.Point MapPoint(WorldTime.gRPC.Point request)
        {
            return new WorldTime.Calculations.Point()
            {
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };
        }
    }
}
