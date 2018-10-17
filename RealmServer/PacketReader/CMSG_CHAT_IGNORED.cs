using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CHAT_IGNORED : Common.Network.PacketReader
    {
        public UInt64 Uid;

        public CMSG_CHAT_IGNORED(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHAT_IGNORED] Uid: {Uid}");
#endif
        }
    }
}