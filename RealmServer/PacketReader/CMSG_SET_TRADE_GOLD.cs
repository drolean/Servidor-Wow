using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_TRADE_GOLD : Common.Network.PacketReader
    {
        public UInt32 Gold;

        public CMSG_SET_TRADE_GOLD(byte[] data) : base(data)
        {
            Gold = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_TRADE_GOLD] Gold: {Gold}");
#endif
        }
    }
}