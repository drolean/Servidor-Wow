using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PETITION_SHOW_SIGNATURES : Common.Network.PacketReader
    {
        public UInt64 PetitionUid;

        public CMSG_PETITION_SHOW_SIGNATURES(byte[] data) : base(data)
        {
            PetitionUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PETITION_SHOW_SIGNATURES] PetitionUid: {PetitionUid}");
#endif
        }
    }
}