using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_STOP_ATTACK : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_PET_STOP_ATTACK(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_STOP_ATTACK] Uid: {Uid}");
#endif
        }
    }
}
