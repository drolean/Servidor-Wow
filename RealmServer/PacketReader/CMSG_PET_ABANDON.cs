using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_ABANDON : Common.Network.PacketReader
    {
        public UInt64 Uid;

        public CMSG_PET_ABANDON(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_ABANDON] Uid: {Uid}");
#endif
        }
    }
}