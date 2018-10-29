using System;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_QUERY_TIME_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_QUERY_TIME_RESPONSE() : base(RealmEnums.SMSG_QUERY_TIME_RESPONSE)
        {
            var baseDate = new DateTime(1970, 1, 1);
            var ts = DateTime.Now - baseDate;

            Write(Convert.ToUInt32(ts.TotalSeconds)); // Time
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return ((dateTime.Year - 100) << 24) | (dateTime.Month << 20) | ((dateTime.Day - 1) << 14) |
                   ((int) dateTime.DayOfWeek << 11) | (dateTime.Hour << 6) | dateTime.Minute;
        }

        /// <summary>Gets the time since the Unix epoch.</summary>
        /// <returns>the time since the unix epoch in seconds</returns>
        public static uint GetEpochTime()
        {
            return (uint) ((ulong) (DateTime.UtcNow.Ticks - 621355968000000000L) / 10000000UL);
        }
    }
}
