using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_QUESTGIVER_HELLO : Common.Network.PacketReader
    {
        public UInt64 Uid;

        public CMSG_QUESTGIVER_HELLO(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_QUESTGIVER_HELLO] Uid: {Uid}");
#endif
        }
    }
}