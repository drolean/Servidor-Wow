using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BANKER_ACTIVATE : Common.Network.PacketReader
    {
        public UInt64 BankerUid;

        public CMSG_BANKER_ACTIVATE(byte[] data) : base(data)
        {
            BankerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BANKER_ACTIVATE] BankerUid: {BankerUid}");
#endif
        }
    }
}