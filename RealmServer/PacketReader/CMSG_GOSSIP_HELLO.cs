using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GOSSIP_HELLO : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_GOSSIP_HELLO(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GOSSIP_HELLO] Uid: {Uid}");
#endif
        }
    }
}
