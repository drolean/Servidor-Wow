using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_PETITION_DECLINE : Common.Network.PacketReader
    {
        public UInt64 PetitionUid;

        public MSG_PETITION_DECLINE(byte[] data) : base(data)
        {
            PetitionUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_PETITION_DECLINE] PetitionUid: {PetitionUid}");
#endif
        }
    }
}