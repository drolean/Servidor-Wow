namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_ACTIVE_MOVER : Common.Network.PacketReader
    {
        public CMSG_SET_ACTIVE_MOVER(byte[] data) : base(data)
        {
            Guid = ReadUInt64();
        }

        public ulong Guid { get; }
    }
}