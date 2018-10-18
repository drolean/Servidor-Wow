using System;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_QUERY_TIME_RESPONSE : Common.Network.PacketServer
    {
        public SMSG_QUERY_TIME_RESPONSE() : base(RealmEnums.SMSG_QUERY_TIME_RESPONSE)
        {
            Write((uint) SecsToTimeBitFields(DateTime.Now)); // Time
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return ((dateTime.Year - 100) << 24) | (dateTime.Month << 20) | ((dateTime.Day - 1) << 14) |
                   ((int) dateTime.DayOfWeek << 11) | (dateTime.Hour << 6) | dateTime.Minute;
        }
    }
}