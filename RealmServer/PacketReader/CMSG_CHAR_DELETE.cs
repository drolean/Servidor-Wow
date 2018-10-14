namespace RealmServer.PacketReader
{
    public sealed class CMSG_CHAR_DELETE : Common.Network.PacketReader
    {
        public ulong Id { get; }

        public CMSG_CHAR_DELETE(byte[] data) : base(data)
        {
            Id = ReadUInt64();
        }
    }
}