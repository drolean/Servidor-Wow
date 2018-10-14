namespace RealmServer.PacketReader
{
    public sealed class CMSG_NAME_QUERY : Common.Network.PacketReader
    {
        public CMSG_NAME_QUERY(byte[] data) : base(data)
        {
            Guid = ReadUInt64();
        }

        public ulong Guid { get; }
    }
}