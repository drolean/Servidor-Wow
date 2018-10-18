using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_PET_NAME_QUERY represents a packet sent by the client when it wants to retrieve other pet information.
    /// </summary>
    public sealed class CMSG_PET_NAME_QUERY : Common.Network.PacketReader
    {
        public uint PetNumber;
        public ulong PetUid;

        public CMSG_PET_NAME_QUERY(byte[] data) : base(data)
        {
            PetNumber = ReadUInt32();
            PetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_NAME_QUERY] PetNumber: {PetNumber} PetUid: {PetUid}");
#endif
        }
    }
}