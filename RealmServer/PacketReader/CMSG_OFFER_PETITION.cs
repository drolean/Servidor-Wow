using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_OFFER_PETITION : Common.Network.PacketReader
    {
        public int PetitionType;
        public UInt64 PetitionUid;
        public UInt64 PlayerUid;

        public CMSG_OFFER_PETITION(byte[] data) : base(data)
        {
            PetitionType = ReadInt32();
            PetitionUid = ReadUInt64();
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_OFFER_PETITION] PetitionType: {PetitionType} PetitionUid: {PetitionUid} " +
                                     $"PlayerUid: {PlayerUid}");
#endif
        }
    }
}