using System;
using Common.Globals;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_LOGIN_SETTIMESPEED : Common.Network.PacketServer
    {
        public SMSG_LOGIN_SETTIMESPEED() : base(RealmEnums.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint) SecsToTimeBitFields(DateTime.Now)); // Time
            Write(0.01666667f); // Speed
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return (dateTime.Year - 100) << 24 | dateTime.Month << 20 | (dateTime.Day - 1) << 14 |
                   (int) dateTime.DayOfWeek << 11 | dateTime.Hour << 6 | dateTime.Minute;
        }
    }
}