using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_UNLEARN : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_PET_UNLEARN(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_UNLEARN] Uid: {Uid}");
#endif
        }
    }
}