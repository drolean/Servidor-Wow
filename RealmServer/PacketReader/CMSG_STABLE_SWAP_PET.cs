using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_STABLE_SWAP_PET : Common.Network.PacketReader
    {
        public ulong NpcUid;
        public uint PetNumber;

        public CMSG_STABLE_SWAP_PET(byte[] data) : base(data)
        {
            NpcUid = ReadUInt64();
            PetNumber = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_STABLE_SWAP_PET] NpcUid: {NpcUid} PetNumber: {PetNumber}");
#endif
        }
    }
}