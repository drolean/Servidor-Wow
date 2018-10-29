using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TURN_IN_PETITION : Common.Network.PacketReader
    {
        public ulong PetitionUid;

        public CMSG_TURN_IN_PETITION(byte[] data) : base(data)
        {
            PetitionUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TURN_IN_PETITION] PetitionUid: {PetitionUid}");
#endif
        }
    }
}
