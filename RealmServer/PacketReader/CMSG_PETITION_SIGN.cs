using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PETITION_SIGN : Common.Network.PacketReader
    {
        public UInt64 PetitionUid;

        public CMSG_PETITION_SIGN(byte[] data) : base(data)
        {
            PetitionUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PETITION_SIGN] PetitionUid: {PetitionUid}");
#endif
        }
    }
}