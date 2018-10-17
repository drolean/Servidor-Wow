using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PETITION_QUERY : Common.Network.PacketReader
    {
        public int PetitionType;
        public UInt64 PetitionUid;

        public CMSG_PETITION_QUERY(byte[] data) : base(data)
        {
            PetitionType = ReadInt32();
            PetitionUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PETITION_QUERY] PetitionType: {PetitionType} PetitionUid: {PetitionUid}");
#endif
        }
    }
}