using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GOSSIP_SELECT_OPTION : Common.Network.PacketReader
    {
        public int Option;
        public ulong Uid;

        public CMSG_GOSSIP_SELECT_OPTION(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            Option = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GOSSIP_SELECT_OPTION] Uid: {Uid} Option: {Option}");
#endif
        }
    }
}
