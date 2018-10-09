namespace RealmServer.PacketReader
{
    public sealed class CMSG_PING : Common.Network.PacketReader
    {
        public CMSG_PING(byte[] data) : base(data)
        {
            Ping = ReadUInt32();
            Latency = ReadUInt32();
        }

        public uint Ping { get; }
        public uint Latency { get; }
    }
}