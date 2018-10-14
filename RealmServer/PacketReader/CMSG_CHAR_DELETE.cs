namespace RealmServer.PacketReader
{
    public sealed class CMSG_CHAR_DELETE : Common.Network.PacketReader
    {
        public CMSG_CHAR_DELETE(byte[] data) : base(data)
        {
            Id = ReadUInt64();
        }

        public ulong Id { get; }
    }
}