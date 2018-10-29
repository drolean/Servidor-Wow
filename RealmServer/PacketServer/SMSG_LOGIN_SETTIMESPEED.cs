using System;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_LOGIN_SETTIMESPEED represents a message sent by the server to define the world timespeed.
    /// </summary>
    public sealed class SMSG_LOGIN_SETTIMESPEED : Common.Network.PacketServer
    {
        /// <summary>
        ///     Sends the world time speed to the client.
        /// </summary>
        /// <remarks>This packet tells the client about the "speed" of time in the game world.</remarks>
        /// <remarks>Usually, this speed is equivalent to real-life.</remarks>
        public SMSG_LOGIN_SETTIMESPEED() : base(RealmEnums.SMSG_LOGIN_SETTIMESPEED)
        {
            var baseDate = new DateTime(1970, 1, 1);
            var ts = DateTime.Now - baseDate;

            Write(Convert.ToUInt32(ts.TotalSeconds)); // Time
            Write(0.01666667f); // Speed
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return ((dateTime.Year - 100) << 24) | (dateTime.Month << 20) | ((dateTime.Day - 1) << 14) |
                   ((int) dateTime.DayOfWeek << 11) | (dateTime.Hour << 6) | dateTime.Minute;
        }
    }
}
