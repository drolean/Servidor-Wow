using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_RENAME : Common.Network.PacketReader
    {
        public UInt64 Uid;
        public string Name;

        public CMSG_PET_RENAME(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            Name = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_RENAME] Uid: {Uid} Name: {Name}");
#endif
        }
    }
}